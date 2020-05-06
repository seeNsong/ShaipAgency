using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShaipAgency.Model;
using ShaipAgency.Model.Request;
using ShaipAgency.Model.Standards;
using ShaipAgency.Model.User;


namespace ShaipAgency.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }



        // 기준정보 DbSet
        public DbSet<StdRequestCodeModel> TB_STD_REQUEST_CODE { get; set; }
        public DbSet<StdRequestStatusCodeModel> TB_STD_REQUEST_STATUS_CODE { get; set; }
        public DbSet<StdRequestStatusRouteInfoModel> TB_STD_REQUEST_STATUS_ROUTE_INFO { get; set; }
        public DbSet<StdEventCodeModel> TB_STD_EVENT_CODE { get; set; }


        // 의뢰요청 DbSet
        public DbSet<ReqMastersModel> TB_REQ_MASTERS { get; set; }
        public DbSet<ReqDetailsDepositModel> TB_REQ_DETAILS_DEPOSIT { get; set; }

        public DbSet<ReqTimestampsModel> TB_REQ_TIMESTAMPS { get; set; }


        // 유저설정 DbSet
        public DbSet<UserActivityModel> TB_USER_ACTIVITY { get; set; }
        public DbSet<UserPassbookModel> TB_USER_PASSBOOK { get; set; }



        /// <summary>
        /// 조합키 선언 
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<StdRequestStatusRouteInfoModel>()
                .HasKey(c => new { c.RequestCode, c.EventCode });

            builder.Entity<UserPassbookModel>()
                .HasKey(c => new { c.RequestNo, c.EventCode });            
        }

    }
}
