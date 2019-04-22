using MyVote.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyVote.UIForms.ViewModels
{
    public class MainViewModel
    {
        private static MainViewModel instance;

        public TokenResponse Token { get; set; }

        public LoginViewModel Login { get; set; }

        public VotingEventsViewModel VotingEvents { get; set; }
       

        public MainViewModel()
        {
            instance = this;
        }

        public static MainViewModel GetInstance()
        {
            if(instance==null)
            {
                return new MainViewModel();
            }
            return instance;
        }
    }
}
