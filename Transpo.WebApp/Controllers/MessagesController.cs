using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Transpo.AppServices.DTOs;
using Transpo.WebApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Transpo.WebApp.Controllers
{
    public class MessagesController : BaseController
    {
        // GET: Messages
        public ActionResult Index()
        {
            return View();
        }

        //
        // POST: Messages/SendMessage
        [HttpPost]
        [Authorize]
        public void SendMessage(MessageViewModel message)
        {
            var userId = UserManager.FindById(User.Identity.GetUserId()).User.id;
            MessageDto mDto = new MessageDto
            {
                RecipientUserId = message.RecipientId,
                SenderUserId = userId,
                MessageBody = message.Body,
                Subject = message.Subject,
            };

            _messageService.SendMessage(mDto);
        }

        //
        // GET: Messages/Conversation
        [Authorize]
        public ActionResult Conversation(int id = 0)
        {
            if (id == 0)
                throw new ArgumentException();

            var userId = UserManager.FindById(User.Identity.GetUserId()).User.id;
            var messages = _messageService.GetMessagesBetweenUsers(userId, id);

            List<MessageViewModel> model = new List<MessageViewModel>();
            foreach (var m in messages)
            {
                m.Sender = _userService.GetUserById(m.SenderId);
                m.Recipient = _userService.GetUserById(m.RecipientId);
                model.Add(new MessageViewModel(m));
            }

            return View(model);
        }

        //
        // GET: Messages/ConversationList
        [Authorize]
        public ActionResult ConversationList()
        {
            var userId = UserManager.FindById(User.Identity.GetUserId()).User.id;
            ViewBag.UserId = userId;

            var messages = _messageService.GetMessagesForUser(userId);

            List<MessageViewModel> model = new List<MessageViewModel>();
            foreach (var m in messages)
            {
                if (m.SenderId == userId)
                {
                    var temp = m.SenderId;
                    m.SenderId = m.RecipientId;
                    m.RecipientId = temp;
                }
                
                m.Sender = _userService.GetUserById(m.SenderId);
                m.Recipient = _userService.GetUserById(m.RecipientId);
                model.Add(new MessageViewModel(m));
            }

            return View(model);
        }

        //
        // GET: Messages/SendMessage
        [HttpGet]
        [Authorize]
        public ActionResult SendMessage(int id = 0)
        {
            if (id == 0)
                throw new ArgumentException();

            var userId = UserManager.FindById(User.Identity.GetUserId()).User.id;
            var ride = _rideService.GetById(id);
            var cp = _rideService.GetRidesSortedCriticalPoints(id);
            string subject = cp.First().Name + " -> " + cp.Last().Name;

            MessageViewModel model = new MessageViewModel
            {
                RecipientId = id,
                SenderId = userId,
                Subject = subject,
                Recipient = new UserViewModel(ride.Driver)
            };

            return View(model);
        }
    }
}