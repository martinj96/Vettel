﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Transpo.AppServices.DTOs;
using Transpo.WebApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Configuration;

namespace Transpo.WebApp.Controllers
{
    public class MessagesController : BaseController
    {
        // GET: Messages
        public ActionResult Index()
        {
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["Messages"]) == false)
            {
                throw new NotImplementedException();
            }

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

            if (message.Body.Length > 0)
            {
                Service.GetMessageService().SendMessage(mDto);
            }
        }

        //
        // GET: Messages/Conversation
        [Authorize]
        public ActionResult Conversation(int id = 0)
        {
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["Messages"]) == false)
            {
                throw new NotImplementedException();
            }

            if (id == 0)
                throw new ArgumentException();

            var userId = UserManager.FindById(User.Identity.GetUserId()).User.id;
            var messages = Service.GetMessageService().GetMessagesBetweenUsers(userId, id, markAsRead: true);

            List<MessageViewModel> model = new List<MessageViewModel>();
            foreach (var m in messages)
            {
                m.Sender = Service.GetUserService().GetUserById(m.SenderId);
                m.Recipient = Service.GetUserService().GetUserById(m.RecipientId);
                model.Add(new MessageViewModel(m));
            }

            return View(model);
        }

        //
        // GET: Messages/ConversationList
        [Authorize]
        public ActionResult ConversationList()
        {
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["Messages"]) == false)
            {
                throw new NotImplementedException();
            }

            var userId = UserManager.FindById(User.Identity.GetUserId()).User.id;
            ViewBag.UserId = userId;

            var messages = Service.GetMessageService().GetMessagesForUser(userId);

            List<MessageViewModel> model = new List<MessageViewModel>();
            foreach (var m in messages)
            {
                if (m.SenderId == userId)
                {
                    var temp = m.SenderId;
                    m.SenderId = m.RecipientId;
                    m.RecipientId = temp;
                }
                
                m.Sender = Service.GetUserService().GetUserById(m.SenderId);
                m.Recipient = Service.GetUserService().GetUserById(m.RecipientId);
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
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["Messages"]) == false)
            {
                throw new NotImplementedException();
            }

            if (id == 0)
                throw new ArgumentException();

            var userId = UserManager.FindById(User.Identity.GetUserId()).User.id;
            var ride = Service.GetRideService().GetById(id);
            var cp = Service.GetRideService().GetRidesSortedCriticalPoints(id);
            string subject = cp.First().Name + " -> " + cp.Last().Name;

            MessageViewModel model = new MessageViewModel
            {
                RecipientId = ride.DriverId,
                SenderId = userId,
                Subject = subject,
                Recipient = new UserViewModel(ride.Driver)
            };

            return View(model);
        }
    }
}