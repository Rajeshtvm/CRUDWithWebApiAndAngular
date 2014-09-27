using CRUDWithWebApiAndAngular.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CRUDWithWebApiAndAngular.Controllers
{
    public class FriendController : ApiController
    {
        private FriendsContext db = new FriendsContext();

        [HttpGet]
        public IEnumerable<Friend> Get()
        {
            return db.Friends.AsEnumerable();
        }

        public Friend Get(int id)
        {
            Friend friend = db.Friends.Find(id);
            if (friend == null)
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));

            return friend;
        }

        public HttpResponseMessage Post(Friend friend)
        {
            if (ModelState.IsValid)
            {
                db.Friends.Add(friend);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, friend);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = friend.FriendId }));
                return response;
            }
            else
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        public HttpResponseMessage Put(int id, Friend friend)
        {
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

            if (id != friend.FriendId)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            db.Entry(friend).State = System.Data.EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Delete(int id)
        {
            Friend friend = db.Friends.Find(id);
            if (friend == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);
            db.Friends.Remove(friend);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, friend);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
