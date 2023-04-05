using Ardalis.GuardClauses;
using AutoMapper;
using MongoDB.Driver;
using Users.Business.Gateway.Repositories;
using Users.Domain.Commands;
using Users.Domain.Common;
using Users.Domain.Entities;
using Users.Infrastructure.Entities;
using Users.Infrastructure.Interfaces;

namespace Users.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<UserMongo> _usersCollection;
        private readonly IMapper _mapper;

        public UserRepository(IContext context, IMapper mapper)
        {
            _usersCollection = context.Users;
            _mapper = mapper;
        }

        public async Task<NewUser> CreateUserAsync(User user)
        {
            User.SetDetailsUserEntity(user);
            var userToCreate = _mapper.Map<UserMongo>(user);

            Guard.Against.Null(userToCreate, nameof(userToCreate));
            Guard.Against.NullOrEmpty(userToCreate.UidUser, nameof(userToCreate.UidUser));
            Guard.Against.NullOrEmpty(userToCreate.UserName, nameof(userToCreate.UserName));
            Guard.Against.NullOrEmpty(userToCreate.Email, nameof(userToCreate.Email));
            Guard.Against.EnumOutOfRange(userToCreate.Role, nameof(userToCreate.Role));
            Guard.Against.EnumOutOfRange(userToCreate.StateUser, nameof(userToCreate.StateUser));

            await _usersCollection.InsertOneAsync(userToCreate);
            return _mapper.Map<NewUser>(userToCreate);
        }

        public async Task<User> DeleteUserAsync(string uidUser)
        {
            var userToDelete = await _usersCollection.Find(u => u.UidUser == uidUser
                    && u.StateUser == Enums.StateUser.Active
                    || u.StateUser == Enums.StateUser.Inactive).FirstOrDefaultAsync();

            if (Guard.Against.Null(userToDelete, nameof(userToDelete),
                    $"There isn't an user available with this uidUser: {uidUser}.") != null)
            {
                userToDelete.SetStateUser(Enums.StateUser.Eliminated);
                await _usersCollection.FindOneAndReplaceAsync(u => u.UidUser == uidUser, userToDelete);
            }
            return _mapper.Map<User>(await _usersCollection.Find(u => u.UidUser == uidUser).FirstOrDefaultAsync());
        }

        public async Task<User> GetUserByIdAsync(string uidUser)
        {
            var user = _mapper.Map<User>(await _usersCollection.Find(u => u.UidUser == uidUser
                    && u.StateUser == Enums.StateUser.Active).FirstOrDefaultAsync());

            return user ?? _mapper.Map<User>(Guard.Against.Null(user, nameof(user),
                            $"There isn't an user available with this uidUser: {uidUser}."));
        }

        public async Task<List<User>> GetUsersByIncriptionIdAsync(string uidUser)
        {
            var usersCursor = await _usersCollection.FindAsync(u => u.UidUser == uidUser
                            && u.StateUser == Enums.StateUser.Active);
            var usersList = _mapper.Map<List<User>>(usersCursor.ToList());

            return usersList.Count == 0
                    ? _mapper.Map<List<User>>(Guard.Against.NullOrEmpty(usersList, nameof(usersList),
                        $"There aren't users available with this inscription ID: {uidUser}."))
                    : usersList;
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            var userFound = await _usersCollection.Find(u => u.UidUser == user.UidUser).FirstOrDefaultAsync();
            user.SetUserID(userFound.UserID);
            var userToUpdate = _mapper.Map<UserMongo>(user);

            Guard.Against.Null(userToUpdate, nameof(userToUpdate));
            Guard.Against.NullOrEmpty(userToUpdate.UidUser, nameof(userToUpdate.UidUser));
            Guard.Against.NullOrEmpty(userToUpdate.UserName, nameof(userToUpdate.UserName));
            Guard.Against.NullOrEmpty(userToUpdate.Email, nameof(userToUpdate.Email));
            Guard.Against.EnumOutOfRange(userToUpdate.Role, nameof(userToUpdate.Role));
            Guard.Against.EnumOutOfRange(userToUpdate.StateUser, nameof(userToUpdate.StateUser));

            var userUpdated = _usersCollection.FindOneAndReplace(u => u.UidUser == userToUpdate.UidUser
                               && u.StateUser == Enums.StateUser.Active
                               || u.StateUser == Enums.StateUser.Inactive, userToUpdate);

            return userUpdated == null
                    ? _mapper.Map<User>(Guard.Against.Null(userUpdated, nameof(userUpdated),
                            $"There isn't an user available with this uidUser: {user.UidUser}."))
                    : _mapper.Map<User>(await _usersCollection.Find(u => u.UidUser == user.UidUser).FirstOrDefaultAsync());
        }
    }
}
