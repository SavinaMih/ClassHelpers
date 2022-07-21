using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClassHelpers.Models.Domain;
using ClassHelpers.Models.ViewModels;
using ClassHelpers.Repositories;

namespace ClassHelpers.Models.BusinessLogic
{
    public class ChatLogic
    {
        IChatRepository chatRepository = new ChatRepository();
        IContactRepository contactRepository = new ContactRepository();
        IAccountRepository accountRepository = new AccountRepository();

        public IEnumerable<ChatMessage> GetLastMessages(int accountId)
        {
            IEnumerable<Chat> chats = chatRepository.GetAllChats(accountId); 
            List<ChatMessage> lastChatMessages = new List<ChatMessage>();

            foreach (Chat c in chats)
            {
                Message latestMessage = c.Messages.OrderByDescending(m => m.MessageSent).FirstOrDefault(); 
                ChatMessage message = CreateMessageModel(c, accountId, latestMessage); 
                lastChatMessages.Add(message);
            }
            return lastChatMessages;
        }

        private ChatMessage CreateMessageModel(Chat c, int accountId, Message latestMessage)
        {
            ChatMessage message = new ChatMessage();
            message.Message = latestMessage;          
            bool senderIsReceiver = latestMessage.SenderIsReceiver; 
            message.RemoveChatAvailable = false;

            if (c.SenderId == accountId) 
            {
                string contactName = contactRepository.GetContact(c.SenderId, c.ReceiverId).ContactName;
                message.OtherAccountContactName = contactName;

                if (senderIsReceiver) message.LastSender = contactName;            
                else YouAreTheLastSender(message); 
            }
            else 
            {
                Contact contact = contactRepository.GetContact(c.ReceiverId, c.SenderId);
                string unknownContactMobileNumber = accountRepository.GetAccount(c.SenderId).MobileNumber;

                if (contact != null)
                {
                    message.OtherAccountContactName = contact.ContactName; 
                    if (senderIsReceiver) YouAreTheLastSender(message); 
                    else message.LastSender = contact.ContactName;
                }
                else
                {
                    message.OtherAccountContactName = unknownContactMobileNumber; 
                    if (senderIsReceiver) YouAreTheLastSender(message); 
                    else message.LastSender = unknownContactMobileNumber; 
                }
            }
            return message;
        }

        private void YouAreTheLastSender(ChatMessage message)
        {
            message.LastSender = "You";
            message.RemoveChatAvailable = true;
        }

        public string GetContactName(Chat chat, int accountId)
        {
            string contactName = "";

            if (chat.SenderId == accountId)
            {
                contactName = contactRepository.GetContact(chat.SenderId, chat.ReceiverId).ContactName;
            }
            else
            {
                Contact contact = contactRepository.GetContact(chat.ReceiverId, chat.SenderId);
                string unknownContactMobileNumber = accountRepository.GetAccount(chat.SenderId).MobileNumber;

                if (contact != null) contactName = contact.ContactName;
                else contactName = unknownContactMobileNumber;
            }
            return contactName;
        }

        public List<ChatMessage> GetAllMessages(Chat chat, int accountId)
        {
            
            IEnumerable<Message> messagesInput = chatRepository.GetAllMessages(chat.ChatId);
            List<ChatMessage> messagesOutput = new List<ChatMessage>();

            foreach (Message m in messagesInput)
            {
                ChatMessage message = CreateMessageModel(chat, accountId, m);
                messagesOutput.Add(message);
            }
            return messagesOutput;
        }

        public void AddMessage(string text, int accountId, Chat chat)
        {
            Message message = new Message();
            message.ChatId = chat.ChatId;
            message.MessageSent = DateTime.Now;
            message.TextMessage = text;

            
            if (chat.ReceiverId == accountId) message.SenderIsReceiver = true;
            else message.SenderIsReceiver = false;

            chatRepository.AddMessage(message);
        }
    }
}
