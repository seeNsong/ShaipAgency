
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using ShaipAgency.Model.Request;
using ShaipAgency.Model.User;

/// <summary>
/// 기준정보 모델(TABLE) 
/// </summary>
namespace ShaipAgency.Model.Standards
{

    /// <summary>
    /// 의뢰요청 코드 테이블
    /// </summary>
    [Table("TB_STD_REQUEST_CODE")]
    public class StdRequestCodeModel
    {
        /// <summary>
        /// 의뢰요청 코드
        /// </summary>
        [Key, Column("RequestCode", TypeName = "Char(2)")]
        public string RequestCode { get; set; }

        /// <summary>
        /// 의뢰요청 이름
        /// </summary>
        [Required, Column("RequestName", TypeName = "NvarChar(20)")]
        public string RequestName { get; set; }

        /// <summary>
        /// 사용여부. 기준정보는 등록되면 삭제하지 않음
        /// </summary>
        [Required, Column("UseYN", TypeName = "bit")]
        public bool UseYN { get; set; }

        
        [Required]
        public int CreationUserId { get; set; }


        [Column("CreationDateTime", TypeName = "datetime")]
        public DateTime CreationDateTime { get; set; }


        /// <summary>
        /// Relation 선언
        /// </summary>
        public ICollection<StdRequestStatusRouteInfoModel> StdRequestStatusRouteInfoModel { get; set; }
        public ICollection<ReqMastersModel> ReqMastersModel { get; set; }

    }



    /// <summary>
    /// 의뢰상태(진도) 코드 테이블
    /// </summary>
    [Table("TB_STD_REQUEST_STATUS_CODE")]
    public class StdRequestStatusCodeModel
    {

        /// <summary>
        /// 의뢰상태 코드
        /// </summary>
        [Key, Column("RequestStatusCode", TypeName = "char(2)")]
        public string RequestStatusCode { get; set; }

        /// <summary>
        /// 의뢰상태 이름
        /// </summary>
        [Required, Column("RequestStatusName", TypeName = "NvarChar(20)")]
        public string RequestStatusName { get; set; }

        /// <summary>
        /// 사용여부
        /// </summary>
        [Required, Column("UseYN", TypeName = "bit")]
        public bool UseYN { get; set; }

        /// <summary>
        /// 알람 우선순위 ( 높을 수록 우선순위 높음)
        /// </summary>
        [Required]
        public int RequestAlertPriority { get; set; }


        [Required]
        public int CreationUserId { get; set; }


        [Column("CreationDateTime", TypeName = "datetime")]
        public DateTime CreationDateTime { get; set; }


        /// <summary>
        /// Relation 선언
        /// </summary>
        public ICollection<StdRequestStatusRouteInfoModel> StdRequestStatusRouteInfoModel { get; set; }
        public ICollection<ReqDetailsDepositModel> ReqDetailsDepositModel { get; set; }
    }


    /// <summary>
    /// 의뢰 상태 변화 라우팅 테이블
    /// </summary>
    [Table("TB_STD_REQUEST_STATUS_ROUTE_INFO")]
    public class StdRequestStatusRouteInfoModel
    {
        /// <summary>
        /// Request Code
        /// </summary>
        //Key, ForeignKey -> Db Project
        [ForeignKey("StdRequestCodeModel")]
        public string RequestCode { get; set; }
        public StdRequestCodeModel StdRequestCodeModel { get; set; }


        /// <summary>
        /// Request Status Code
        /// </summary>
        //Key, ForeignKey -> Db Project        
        [ForeignKey("StdEventCodeModel")]
        public string EventCode { get; set; }
        public StdEventCodeModel StdEventCodeModel { get; set; }



        /// <summary>
        /// Next Request Status Code
        /// </summary>        
        [ForeignKey("StdRequestStatusCodeModel")]        
        public string NextStatusCode { get; set; }
        public StdRequestStatusCodeModel StdRequestStatusCodeModel { get; set; }


        /// <summary>
        /// Foreign키 선언, Key선언은 FluentAPI
        /// </summary>
        [Required]
        public int CreationUserId { get; set; }


        /// <summary>
        /// Foreign키 선언, Key선언은 FluentAPI
        /// </summary>
        [Column("CreationDateTime", TypeName = "datetime")]
        public DateTime CreationDateTime { get; set; }

    }


    /// <summary>
    /// 이벤트를 정의하는 테이블
    /// </summary>
    [Table("TB_STD_EVENT_CODE")]
    public class StdEventCodeModel
    {
        /// <summary>
        /// 이벤트 코드
        /// </summary>
        [Key, Column("EventCode",TypeName ="char(2)")]
        public string EventCode { get; set; }

        /// <summary>
        /// 이벤트 이름
        /// </summary>
        [Required, Column("EventName", TypeName = "Nvarchar(20)")]
        public string EventName { get; set; }

        /// <summary>
        /// 사용여부
        /// </summary>
        [Column("UseYN",TypeName ="bit")]
        public bool UseYN { get; set; }

        /// <summary>
        /// 이벤트설명
        /// </summary>
        [Column("EventDescription",TypeName ="Nvarchar(256)")]
        public string EventDescription { get; set; }

        /// <summary>
        /// Relation 선언
        /// </summary>
        public ICollection<UserPassbookModel> UserPassbookModel { get; set; }
        public ICollection<StdRequestStatusRouteInfoModel> StdRequestStatusRouteInfoModel { get; set; }
    }
}
