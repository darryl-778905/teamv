using System;
using System.ComponentModel.DataAnnotations;

namespace MobilePoll.Infrastructure.TestShell.DataModel
{
    public class GreetingLog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime OccuredAt { get; set; }
    }
}