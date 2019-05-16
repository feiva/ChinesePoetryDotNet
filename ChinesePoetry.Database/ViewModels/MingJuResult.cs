using AutoMapper.Configuration.Conventions;
using ChinesePoetry.Database.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChinesePoetry.Database.ViewModels
{
    public class MingJuResult
    {
        /// <summary>
        /// 
        /// </summary>
        public int sumCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int sumPage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int currentPage { get; set; }
        /// <summary>
        /// 经典名句_古诗文名句_古诗文网
        /// </summary>
        public string pageTitle { get; set; }
        /// <summary>
        /// 不限
        /// </summary>
        public string keyStr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<MingjusItem> mingjus { get; set; }
        /// <summary>
        /// 名句首页
        /// </summary>
        public string masterTitle { get; set; }
    }

    public class MingjusItem
    {
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MapTo(nameof(MingJu.OId))]
        [JsonProperty("id")]
        public int oid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [MapTo(nameof(MingJu.OIdNew))]
        public string idnew { get; set; }
        /// <summary>
        /// 山有木兮木有枝，心悦君兮君不知。
        /// </summary>
        [MapTo(nameof(MingJu.Content))]
        public string nameStr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string classStr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [MapTo(nameof(MingJu.OShiId))]
        public int shiID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [MapTo(nameof(MingJu.OShiIdNew))]
        public string shiIDnew { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int exing { get; set; }
        /// <summary>
        /// 佚名
        /// </summary>
        [MapTo(nameof(MingJu.Author))]
        public string author { get; set; }
        /// <summary>
        /// 越人歌
        /// </summary>
        [MapTo(nameof(MingJu.SourceName))]
        public string shiName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ipStr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string isShiwen { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string gujiyiwen { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zhangjieID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zhangjieNameStr { get; set; }
    }

}
