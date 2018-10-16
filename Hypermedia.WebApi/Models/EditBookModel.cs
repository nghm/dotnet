﻿using System.ComponentModel.DataAnnotations;

namespace Hypermedia.WebApi.Models
{
    public class EditBookModel
    {
        [Required, MaxLength(255)]
        public string Title { get; set; }

        [Required, MaxLength(255)]
        public string Description { get; set; }
    }
}