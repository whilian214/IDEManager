using System;
using System.Collections.Generic;
using IDEManager.Models;

namespace IDEManager.Services
{
    public class TrackingService
    {
        private readonly List<WorkSession> _sessions = new();
        private DateTime? _startTime;
        private int? _activeProjectId;
        private int? _activeIdeId;

        public void StartTracking(int projectId, int ideId)
        {
            _startTime = DateTime.Now;
            _activeProjectId = projectId;
            _activeIdeId = ideId;
        }

        public void StopTracking()
        {
            if (_startTime == null || _activeProjectId == null || _activeIdeId == null)
                return;

            var elapsed = (long)(DateTime.Now - _startTime.Value).TotalSeconds;

            _sessions.Add(new WorkSession
            {
                Id = _sessions.Count + 1,
                ProjectId = _activeProjectId.Value,
                IDEId = _activeIdeId.Value,
                StartedAt = _startTime.Value,
                EndedAt = DateTime.Now,
                TotalSeconds = elapsed
            });

            _startTime = null;
            _activeProjectId = null;
            _activeIdeId = null;
        }

        public List<WorkSession> GetSessions()
        {
            return _sessions;
        }

        public long GetTotalSecondsForProject(int projectId)
        {
            long total = 0;

            foreach (var session in _sessions)
            {
                if (session.ProjectId == projectId)
                {
                    total += session.TotalSeconds;
                }
            }

            return total;
        }
    }
}
