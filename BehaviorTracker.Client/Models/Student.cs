using System;
using System.Collections.Generic;
using Microsoft.JSInterop;

namespace BehaviorTracker.Client.Models
{
    public class Student : Copyable<Student>
    {
        public long StudentKey { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public IEnumerable<Goal> Goals { get; set; }
    }
}