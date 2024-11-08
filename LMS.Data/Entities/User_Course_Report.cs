﻿using System.ComponentModel.DataAnnotations;

namespace LMS.Data.Entities
{
    public class User_Course_Report
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public Guid CourseId { get; set; }
        public Course? Course { get; set; }
    }
}