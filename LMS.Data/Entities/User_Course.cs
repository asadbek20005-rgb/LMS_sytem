﻿using System.ComponentModel.DataAnnotations;

namespace LMS.Data.Entities
{
    public class User_Course
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }


        public Guid CourseId { get; set; }
        public Course? Course { get; set; }


        public bool IsOwner { get; set; }
        public bool IsPayed { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}