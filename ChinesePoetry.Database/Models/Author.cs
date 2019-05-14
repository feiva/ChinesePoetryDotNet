using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ChinesePoetry.Database.Models
{
    public class Author
    {
        [Key]
        public long Id { get; set; }
        /// <summary>
        /// 名字
        /// </summary>
        [StringLength(255)]
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 朝代
        /// </summary>
        public string Dynasty { get; set; }
    }
}
