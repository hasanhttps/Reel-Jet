using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reel_Jet.Models {
    public class Notification {

        // Private Fields

        private readonly Guid _id;
        private DateTime _dateTime;
        private string? _title;
        private string? _text;
        public string? _fromUser;

        // Properties

        public Guid Id { get { return _id; } }
        public DateTime DateTime { get { return _dateTime; } set { _dateTime = value; } }
        public string? Title { get { return _title;} set { _title = value; } }
        public string? Text { get { return _text; } set { _text = value; } }
        public string? FromUser { get { return _fromUser; } set { _fromUser = value; } }

        // Constructors

        public Notification() { 
            _id  = Guid.NewGuid();
            _dateTime = DateTime.Now;
        }
        public Notification(string? title, string? text, string? fromUser) : this() {
            Title = title;
            Text = text;
            FromUser = fromUser;
        }

        // Functions

        public override string ToString() {
            string notification = $"Date : {DateTime}\nFrom : {FromUser}\nTitle : {Title}\nText : {Text}\n";
            return notification;
        }
    }

}
