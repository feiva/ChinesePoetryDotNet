using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ChinesePoetry.Database.Models
{
    /// <summary>
    /// 诗词
    /// </summary>
    public class Poetry
    {
        [Key]
        public long Id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        [StringLength(255)]
        [Required]
        public string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        [Required]
        public string Content { get; set; }
        /// <summary>
        /// 朝代
        /// </summary>
        [StringLength(10)]
        public string Dynasty { get; set; }
        /// <summary>
        /// 作者名
        /// </summary>
        [StringLength(255)]
        public string AuthorName { get; set; }
        /// <summary>
        /// 音律
        /// </summary>
        public string Rhythm { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        [Required]
        public PoetryType Type { get; set; }
        /// <summary>
        /// 作者id
        /// </summary>
        public long? AuthorId { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public Author Author { get; set; }
    }

    public enum PoetryType
    {
        Shi = 1,
        Ci = 2
    }
}
