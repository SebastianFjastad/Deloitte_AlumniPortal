using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AlumniPortal.DbContext;
using AlumniPortal.Entities;
using AlumniPortal.Models;

namespace AlumniPortal.Repositories
{
    public class EventRepository
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public List<Event> GetEvents()
        {
            return db.Events.Include(e => e.EventInvites).OrderBy(e => e.From).ToList();
        }

        public Event GetEvent(int? id)
        {
            var result = db.Events.Include(e => e.EventInvites).Include("EventInvites.User").FirstOrDefault(e => e.EventID == id);

            return result;
        }

        public bool SaveEvent(Event ev)
        {
            try
            {
                var usersInRegion = db.Users.Where(u => u.Region.Contains(ev.Region)).ToList();

                foreach (var user in usersInRegion)
                {
                        db.EventInvites.Add(new EventInvite
                        {
                            Id = user.Id,
                            EventID = ev.EventID,
                            Attending = false,
                            IsApplication = false,
                        });
                }

                db.Events.Add(ev);
                db.SaveChanges();
                return true;
            }
            catch
            {
                //log here if necessary
            }
            return false;
        }

        public bool EditEvent(Event ev)
        {
            if (ev == null) return true;

            db.Entry(ev).State = EntityState.Modified;
            db.SaveChanges();

            return false;
        }

        public List<ApplicationUser> GetUsersNotInEvent(int? eventId)
        {
            List<ApplicationUser> usersNotInEvent = db.Users.Where(u => u.EventInvites.Any(ei => ei.EventID == eventId && ei.Attending != true)).ToList();
            return usersNotInEvent;
        }

        public List<ApplicationUser> GetUserEventApplications(int eventId)
        {
            return db.Users.Where(u => u.EventInvites.Any(e => e.EventID == eventId && e.Attending)).ToList();
        }

        public bool AcceptApplications(List<string> ids, int eventId)
        {
            //var eventInvites = db.EventInvites.Where(e => e.EventID == eventId && e.IsApplication).ToList();

            List<EventInvite> userEventApplications = db.EventInvites.Where(e => ids.Contains(e.Id)).ToList();

            foreach (var ei in userEventApplications)
            {
                ei.IsApplication = false;
                ei.Attending = true;
            }
            db.SaveChanges();
            return false;
        }

        public bool UserAttendEvent(string userId, int eventId, bool isApplication)
        {
            var existingEventInvite = db.EventInvites.FirstOrDefault(e => e.Id == userId && e.EventID == eventId);

            if (existingEventInvite != null)
            {
                existingEventInvite.Attending = true;
                existingEventInvite.IsApplication = false;
            }
            else
            {
                db.EventInvites.Add(new EventInvite
                {
                    Id = userId,
                    EventID = eventId,
                    Attending = false,
                    IsApplication = isApplication
                });
            }

            db.SaveChanges();

            return false;
        }

        public bool DeleteEvent(int? id)
        {
            if (id != null)
            {
                var eventToDelete = db.Events.Find(id);
                db.Events.Remove(eventToDelete);
                db.SaveChanges();
                return false;
            }
            return true;
        }
    }
}