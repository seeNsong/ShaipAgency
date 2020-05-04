using ShaipAgency.Model.Standards;
using ShaipAgency.Model.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShaipAgency.Model.Request
{
    [Table("TB_REQ_MASTERS")]
    public class ReqMastersModel
    {
        /// <summary>
        /// 의뢰번호
        /// </summary>
        [Key, Column("RequestNo",TypeName ="varchar(15)")]
        public string RequestNo { get; set; }


        /// <summary>
        /// 의뢰코드
        /// </summary>
        [ForeignKey("StdRequestCodeModel")]
        public string RequestCode { get; set; }
        public StdRequestCodeModel StdRequestCodeModel { get; set; }


        /// <summary>
        /// 의뢰자 ID
        /// </summary>
        [Required, Column("RequestUserId",TypeName ="int")]        
        public int RequestUserId { get; set; }

        /// <summary>
        /// 의뢰제목(자동생성)
        /// </summary>
        [Column("RequestTitle", TypeName ="Nvarchar(256)")]
        public string RequestTitle { get; set; }


        /// <summary>
        /// 하위 개체 수 (Details), 상품의 경우 여러 상품 등록될 수 있음
        /// </summary>
        [Column("DetailCount", TypeName ="int")]
        public int DetailCount { get; set; }

        /// <summary>
        /// 클레임 수
        /// </summary>
        [Column("RemainClaims", TypeName = "int")]
        public int RemainClaims { get; set; }

        /// <summary>
        /// 국내배송묶음번호. 기본값 = RequestNo
        /// </summary>
        [Column("DeliveryGroupNo", TypeName = "varchar(15)")]
        public int DeliveryGroupNo { get; set; }


        /// <summary>
        /// 수정잠금. true = 주문서 수정 불가. false = 주문서 수정 가능
        /// </summary>
        public bool IsLocked { get; set; }


        /// <summary>
        /// 의뢰요청일
        /// </summary>
        [Column("CreationDateTime", TypeName = "datetime")]
        public DateTime CreationDateTime { get; set; }


        /// <summary>
        /// 의뢰요청일
        /// </summary>
        [Column("ModificationDateTime", TypeName = "datetime")]
        public DateTime ModificationDateTime { get; set; }


        /// <summary>
        /// Relation 선언
        /// </summary>
        public ICollection<ReqDetailsDepositModel> ReqDetailsDepositModel { get; set; }

        /// <summary>
        /// Relation선언
        /// </summary>
        public ICollection<UserPassbookModel> UserPassbookModel { get; set; }

    }

    [Table("TB_REQ_DETAILS_DEPOSIT")]
    public class ReqDetailsDepositModel
    {

        /// <summary>
        /// 의뢰번호
        /// </summary>
        [Key, ForeignKey("ReqMastersModel")]
        public string RequestNo { get; set; }

        /// <summary>
        /// Foreign Key 선언
        /// </summary>
        public ReqMastersModel ReqMastersModel { get; set; }


        /// <summary>
        /// 입금일 또는 출금요청일
        /// </summary>
        [Required,Column("DepositDate",TypeName ="datetime")]
        public DateTime DepositDate { get; set; }


        /// <summary>
        /// 입금액 또는 출금요청액
        /// </summary>
        [Column("Amount",TypeName ="int")]
        public int Amount { get; set; }


        /// <summary>
        /// 입금자 또는 출금계좌 성명
        /// </summary>
        [Column("PersonName",TypeName ="Nvarchar(20)")]
        public string PersonName { get; set; }

        /// <summary>
        /// 출금계좌번호
        /// </summary>
        [Column("AccountNumber", TypeName = "Nvarchar(40)")]
        public string AccountNumber { get; set; }        


        [ForeignKey("StdRequestStatusCodeModel")]
        public string RequestStatusCode { get; set; }
        public StdRequestStatusCodeModel StdRequestStatusCodeModel { get; set; }

    }

    [Table("TB_REQ_TIMESTAMPS")]
    public class ReqTimestampsModel
    {

        /// <summary>
        /// timestamp, Key
        /// </summary>
        [Key, Column("Timestamp", TypeName = "datetime")]
        public DateTime Timestamp { get; set; }


        /// <summary>
        /// Request No.
        /// </summary>
        [Column("RequestNo",TypeName ="varchar(15)")]
        public string RequestNo { get; set; }


        /// <summary>
        /// Detail No.
        /// </summary>
        [Column("DetailNo")]
        public int DetailNo { get; set; }


        /// <summary>
        /// Event Code
        /// </summary>
        [Column("EventCode",TypeName ="char(2)")]
        public string EventCode { get; set; }

        

    }
}
