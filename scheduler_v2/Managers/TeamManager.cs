using scheduler_v2.Models;
using scheduler_v2.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace scheduler_v2.Managers
{
    public class TeamManager
    {
        // takes user ids
        public teams CreateTeam(string Team_Name, int Teamlead)
        {
            teams team = new teams()
            {
                name = Team_Name,
                teamlead_id = Teamlead
            };
            return team;
        }

        public user_teams AddUserTeam(int Team_id, int User_id)
        {
            var user_team = new user_teams
            {
                team_id = Team_id,
                user_id = User_id
            };

            return user_team;
        }

        public void AddUserToTeam(string TeamName, int Teamlead_id, int[] User_ids)
        {
            using(scheduler_v2Entities db = new scheduler_v2Entities())
            {
                UserManager umngr = new UserManager();
                var teamClass = new teams
                {
                    name = TeamName,
                    teamlead_id = Teamlead_id
                };
                db.teams.Add(teamClass);
                db.teams.Attach(teamClass);
                var teamlead = new user_teams
                {
                    team_id = teamClass.id,
                    user_id = Teamlead_id
                };
                db.teams.Add(teamClass);
                db.user_teams.Add(teamlead);

                foreach(int id in User_ids)
                {
                    users person = umngr.GetUser(id);
                    var user_team = new user_teams
                    {
                        team_id = teamClass.id,
                        user_id = id
                    };
                    db.user_teams.Add(user_team);
                }
                
                db.SaveChanges();
            }

        }

        public List<int?> GetTeamIdsByProjectId(int ProjectId)
        {
            using (scheduler_v2Entities db = new scheduler_v2Entities())
            {
                var listOfIds = db.project_teams.Select(x => x.project_id).ToList();
                if(listOfIds.Count < 1)
                {
                    return null;
                }
                else
                {
                    return listOfIds;
                }
                
            }
        }

        public List<teams> GetTeamListByProjectId(int ProjectId)
        {
            using (scheduler_v2Entities db = new scheduler_v2Entities())
            {
                var listOfTeamIds = GetTeamIdsByProjectId(ProjectId);

                var listOfTeams = db.teams.Where(x => listOfTeamIds.Contains(x.id));

                project_teams projectTeam = (project_teams)db.project_teams.Where(x => x.project_id == ProjectId);
                if(listOfTeamIds == null)
                {
                    return null;
                }
                else
                {
                    int teamId = (int)projectTeam.team_id;
                    List<teams> teams = new List<teams>();
                    foreach(var team in teams)
                    {
                        teams.Add(team);
                    }
                    return teams;
                }
                
            }
        }

        //Returns team object by given id and returns null if not found
        public teams GetTeamById(int TeamId)
        {
            using (scheduler_v2Entities db = new scheduler_v2Entities())
            {
                teams team = (teams)db.teams.Where(x => x.id == TeamId);
                if(team == null)
                {
                    return null;
                }
                else
                {
                    return team;
                }
            }
        }

        //Checks if there are matching team names in db
        public bool TeamNameExists(string TeamName)
        {
            bool exists = true;
            using (scheduler_v2Entities db = new scheduler_v2Entities())
            {
                var team = db.teams.Where(x => x.name == TeamName);
                if(team == null) {
                    exists = !exists;
                }
            }
            return exists;
        }
    }
}