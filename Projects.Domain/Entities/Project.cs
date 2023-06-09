﻿using Projects.Domain.Common;

namespace Projects.Domain.Entities
{
    public class Project
    {
        public Guid ProjectID { get; private set; }
        public string LeaderID { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? OpenDate { get; private set; }
        public DateTime? DeadLine { get; private set; }
        public DateTime? CompletedAt { get; private set; }
        public decimal EfficiencyRate { get; private set; }
        public Enums.Phase? Phase { get; private set; }
        public Enums.StateProject StateProject { get; private set; }

        #region constructors
        public Project() { }
        #endregion

        #region methods
        public static Project SetDetailsProjectEntity(Project project)
        {
            project.CreatedAt = DateTime.Now;
            project.EfficiencyRate = 0;
            project.StateProject = Enums.StateProject.Active;
            return project;
        }
        #endregion

        #region setters
        public void SetProjectID(Guid projecID)
        {
            ProjectID = projecID;
        }
        public void SetLeaderID(string leaderID)
        {
            LeaderID = leaderID;
        }
        public void SetName(string name)
        {
            Name = name;
        }
        public void SetDescription(string description)
        {
            Description = description;
        }
        public void SetCreatedAt(DateTime createdAt)
        {
            CreatedAt = createdAt;
        }
        public void SetOpenDate(DateTime? openDate)
        {
            OpenDate = openDate;
        }
        public void SetDeadLine(DateTime? deadLine)
        {
            DeadLine = deadLine;
        }
        public void SetCompletedAt(DateTime? completedAt)
        {
            CompletedAt = completedAt;
        }
        public void SetEfficiencyRate(decimal efficiencyRate)
        {
            EfficiencyRate = efficiencyRate;
        }
        public void SetPhase(Enums.Phase? phase)
        {
            Phase = phase;
        }
        public void SetStateProject(Enums.StateProject stateProject)
        {
            StateProject = stateProject;
        }
        #endregion
    }
}