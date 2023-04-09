using Ardalis.GuardClauses;
using AutoMapper;
using Dapper;
using Projects.Business.Gateway.Repositories;
using Projects.Domain.Common;
using Projects.Domain.Common.Handlers;
using Projects.Domain.DTO.Comment;
using Projects.Domain.Entities;
using Projects.Infrastructure.Gateway;

namespace Projects.Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IDbConnectionBuilder _dbConnectionBuilder;
        private readonly string _tableNameProjects = "Projects";
        private readonly string _tableNameInscriptions = "Inscriptions";
        private readonly string _tableNameComments = "Comments";
        private readonly IMapper _mapper;

        public CommentRepository(IDbConnectionBuilder dbConnectionBuilder, IMapper mapper)
        {
            _dbConnectionBuilder = dbConnectionBuilder;
            _mapper = mapper;
        }

        public async Task<NewCommentDTO> CreateCommentAsync(Comment comment)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();

            var projectFound = (from p in await connection.QueryAsync<Project>($"SELECT * FROM {_tableNameProjects}")
                                where p.ProjectID == comment.ProjectID && p.StateProject == Enums.StateProject.Active
                                && p.Phase == Enums.Phase.Started
                                select p)
                                .SingleOrDefault();

            Guard.Against.Null(projectFound, nameof(projectFound),
                $"There is no a project available with ID: {comment.ProjectID}.");

            var inscriptionFound = (from i in await connection.QueryAsync<Inscription>($"SELECT * FROM {_tableNameInscriptions}")
                                    where i.UidUser == comment.UidUser && i.ProjectID == comment.ProjectID
                                    && i.StateInscription == Enums.StateInscription.Approved
                                    select i)
                                    .SingleOrDefault();

            Guard.Against.Null(inscriptionFound, nameof(inscriptionFound),
                    $"There is no an approved inscription or yours in this project.");

            Comment.SetDetailsCommentEntity(comment);
            var viableCommentDate = DatesHandler.ValidateWithinTheProjectDeadLineNotOpen(comment.CreatedAt, projectFound);
            if (!viableCommentDate)
            {
                Guard.Against.Default(viableCommentDate, nameof(viableCommentDate),
                    $"Assign date: {comment.CreatedAt:dd/MM/yyyy} " +
                    $"is greater than project deadline: {projectFound.DeadLine:dd/MM/yyyy} " +
                    $"or less than project open date: {projectFound.OpenDate:dd/MM/yyyy}.");
            }

            string query = $"INSERT INTO {_tableNameComments} (UidUser, ProjectID, Description, CreatedAt, StateComment) " +
                            $"VALUES (@UidUser, @ProjectID, @Description, @CreatedAt, @StateComment)";

            var result = await connection.ExecuteAsync(query, comment);
            connection.Close();
            return result == 0 ? _mapper.Map<NewCommentDTO>(Guard.Against.Zero(result, nameof(result),
                                     $"The record has not been modified. Rows affected ({result})"))
                                : _mapper.Map<NewCommentDTO>(comment);
        }

        public async Task<UpdateCommentDTO> DeleteCommentAsync(int idComment)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();

            var commentFound = (from c in await connection.QueryAsync<Comment>($"SELECT * FROM {_tableNameComments}")
                                where c.CommentID == idComment
                                && c.StateComment != Enums.StateComment.Deleted
                                select c)
                                .SingleOrDefault();

            Guard.Against.Null(commentFound, nameof(commentFound),
                $"There is no a comment available with ID: {idComment}.");

            var projectFound = (from p in await connection.QueryAsync<Project>($"SELECT * FROM {_tableNameProjects}")
                                where p.ProjectID == commentFound.ProjectID && p.StateProject == Enums.StateProject.Active
                                && p.Phase == Enums.Phase.Started
                                select p)
                                .SingleOrDefault();

            Guard.Against.Null(projectFound, nameof(projectFound),
                $"There is no a project available with ID: {commentFound.ProjectID}.");

            var inscriptionFound = (from i in await connection.QueryAsync<Inscription>($"SELECT * FROM {_tableNameInscriptions}")
                                    where i.UidUser == commentFound.UidUser && i.ProjectID == commentFound.ProjectID
                                    && i.StateInscription == Enums.StateInscription.Approved
                                    select i)
                                    .SingleOrDefault();

            Guard.Against.Null(inscriptionFound, nameof(inscriptionFound),
                    $"There is no an approved inscription or yours in this project.");

            commentFound.SetStateComment(Enums.StateComment.Deleted);

            string query = $"UPDATE {_tableNameComments} SET StateComment = @StateComment " +
                            $"WHERE CommentID = @CommentID";
            var result = await connection.ExecuteAsync(query, commentFound);
            connection.Close();

            return result == 0 ? _mapper.Map<UpdateCommentDTO>(Guard.Against.Zero(result, nameof(result),
                                     $"The record has not been modified. Rows affected ({result})"))
                                : _mapper.Map<UpdateCommentDTO>(commentFound);

        }

        public async Task<List<Comment>> GetAllCommentsAsync()
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();

            var commentsFound = (from c in await connection.QueryAsync<Comment>($"SELECT * FROM {_tableNameComments}")
                                 where c.StateComment != Enums.StateComment.Deleted
                                 select c)
                                .ToList();
            connection.Close();
            return commentsFound.Count == 0
                ? _mapper.Map<List<Comment>>(
                    Guard.Against.NullOrEmpty(commentsFound, nameof(commentsFound),
                        $"There are no comments available."))
                : commentsFound;
        }
    }
}