using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additonal Namespaces
using System.ComponentModel; //ODS
using ChinookSystem.Data.Entities;
using ChinookSystem.Data.POCOs;
using ChinookSystem.DAL;
#endregion


namespace ChinookSystem.BLL
{
    [DataObject]
    public class TrackController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Track> ListTracks()
        {
            using (var context = new ChinookContext())
            {
                // return all records all attributes
                return context.Tracks.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public Track Get_Track(int trackid)
        {
            using (var context = new ChinookContext())
            {
                // return a record and all attributes
                return context.Tracks.Find(trackid);
            }
        }

        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void Add_Track(Track trackinfo)
        {
            using (var context = new ChinookContext())
            {
                // any business rules

                // Any data refinements
                // review of iif
                //composer can be a null string, we do not wish to store an empty string
                trackinfo.Composer = string.IsNullOrEmpty(trackinfo.Composer) ? null : trackinfo.Composer;
                // add the instance of track info to the database
                context.Tracks.Add(trackinfo);
                // commit of the add
                context.SaveChanges();

            }
        }

        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public void Update_Track(Track trackinfo)
        {
            using (var context = new ChinookContext())
            {
                // any business rules

                // Any data refinements
                // review of iif
                //composer can be a null string, we do not wish to store an empty string
                trackinfo.Composer = string.IsNullOrEmpty(trackinfo.Composer) ? null : trackinfo.Composer;
                // update the instance of track info to the database
                context.Entry(trackinfo).State = System.Data.Entity.EntityState.Modified;
                // commit of the update
                context.SaveChanges();

            }
        }

        // The delete is an overloaded method technique
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public void DeleteTrack(Track trackinfo)
        {
            DeleteTrack(trackinfo.TrackId);
        }

        public void DeleteTrack(int trackid)
        {
            using (var context = new ChinookContext())
            {
                // Any business rules

                // find the existing record on the database
                var existing = context.Tracks.Find(trackid);
                // or
                // var existing = Get_Track(trackid);

                // remove track
                context.Tracks.Remove(existing);

                // commit the transaction
                context.SaveChanges();
            }
        }
    }
}
