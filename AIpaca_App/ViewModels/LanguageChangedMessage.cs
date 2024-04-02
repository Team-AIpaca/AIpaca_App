using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIpaca_App.ViewModels
{
    public class LanguageChangedMessage : ValueChangedMessage<string>
    {
        public LanguageChangedMessage(string value) : base(value)
        {
        }
    }
}
