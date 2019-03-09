namespace PositiveEdu.DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class PeContext : DbContext
    {
        public PeContext()
            : base("name=PeContext")
        {
        }

        public virtual DbSet<ADMIN> ADMIN { get; set; }
        public virtual DbSet<ADMIN_ROLE_RELATION> ADMIN_ROLE_RELATION { get; set; }
        public virtual DbSet<MyDocument> MyDocument { get; set; }
        public virtual DbSet<Document> Document { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<ROLE> ROLE { get; set; }
        public virtual DbSet<ROLE_FUNCTION_RELATION> ROLE_FUNCTION_RELATION { get; set; }
        public virtual DbSet<ROLE_FUNCTIONS> ROLE_FUNCTIONS { get; set; }
        public virtual DbSet<SiteMessage> SiteMessage { get; set; }

        public virtual DbSet<AREA> AREA { get; set; }

        /// <summary>
        /// ��Ʒ����
        /// </summary>
        public virtual DbSet<T_Gifts> T_Gifts { get; set; }
        /// <summary>
        /// ��Ʒ�ӱ�
        /// </summary>
        public virtual DbSet<T_GiftsChild> T_GiftChild { get; set; }
        /// <summary>
        /// �����
        /// </summary>
        public virtual DbSet<T_Activity> T_Activity { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        public virtual DbSet<T_Reward> T_Reward { get; set; }
        /// <summary>
        /// ������ӱ�
        /// </summary>
        public virtual DbSet<T_RewardChild> T_RewardChild { get; set; }
        /// <summary>
        /// ���Ա��
        /// </summary>
        public virtual DbSet<T_CustomerActivity> T_CustomerActivity { get; set; }
        /// <summary>
        /// ��Ա����
        /// </summary>
        public virtual DbSet<T_Customer> T_Customer { get; set; }
        /// <summary>
        ///��Ա�ռ���Ϣ��
        /// </summary>
        public virtual DbSet<T_CustomerAccept> T_CustomerAccept { get; set; }
        /// <summary>
        ///��Ա���ּ�¼��
        /// </summary>
        public virtual DbSet<T_CustomerIntegralRecord> T_CustomerIntegralRecord { get; set; }
        /// <summary>
        ///��Ʒ�һ���
        /// </summary>
        public virtual DbSet<T_ExchangeGifts> T_ExchangeGift { get; set; }
        /// <summary>
        ///��Ա��ǩ
        /// </summary>
        public virtual DbSet<T_CustomerTag> T_CustomerTag { get; set; }

        /// <summary>
        ///��Ʒ����
        /// </summary>
        public virtual DbSet<T_GiftsTag> T_GiftsTag { get; set; }

        /// <summary>
        /// ������������Ӽ�¼
        /// </summary>
        public virtual DbSet<T_PrivateGiftsRecord> T_PrivateGiftsRecord { get; set; }
        /// <summary>
        /// �����������¼��
        /// </summary>
        public virtual DbSet<T_OthersGiftsRecord> T_OthersGiftsRecord { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ADMIN>()
                .Property(e => e.PASSWORD)
                .IsUnicode(false);

            modelBuilder.Entity<ADMIN>()
                .Property(e => e.USER_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<ADMIN>()
                .Property(e => e.TOKEN)
                .IsUnicode(false);

            modelBuilder.Entity<ADMIN>()
                .Property(e => e.REAL_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<ADMIN>()
                .Property(e => e.PHONE)
                .IsUnicode(false);

            modelBuilder.Entity<ADMIN>()
                .HasMany(e => e.ADMIN_ROLE_RELATION)
                .WithOptional(e => e.ADMIN)
                .HasForeignKey(e => e.ADMIN_ID);

            modelBuilder.Entity<ADMIN>()
                .HasMany(e => e.Document)
                .WithOptional(e => e.ADMIN)
                .HasForeignKey(e => e.adminId);

            modelBuilder.Entity<ADMIN>()
                .HasMany(e => e.SiteMessage)
                .WithOptional(e => e.ADMIN)
                .HasForeignKey(e => e.adminId);


            modelBuilder.Entity<ROLE>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<ROLE>()
                .Property(e => e.Detail)
                .IsUnicode(false);

            modelBuilder.Entity<ROLE>()
                .HasMany(e => e.ADMIN_ROLE_RELATION)
                .WithOptional(e => e.ROLE)
                .HasForeignKey(e => e.ROLE_ID);

            modelBuilder.Entity<ROLE>()
                .HasMany(e => e.ROLE_FUNCTION_RELATION)
                .WithOptional(e => e.ROLE)
                .HasForeignKey(e => e.ROLE_ID);

            modelBuilder.Entity<ROLE_FUNCTIONS>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<ROLE_FUNCTIONS>()
                .Property(e => e.URL)
                .IsUnicode(false);

            modelBuilder.Entity<ROLE_FUNCTIONS>()
                .HasMany(e => e.ROLE_FUNCTION_RELATION)
                .WithOptional(e => e.ROLE_FUNCTIONS)
                .HasForeignKey(e => e.FUNCTION_ID);




            modelBuilder.Entity<AREA>()
                .Property(e => e.AREA_NAME)
                .IsUnicode(false);




            modelBuilder.Entity<T_Customer>()
    .HasMany(e => e.T_CustomerActivity)
    .WithOptional(e => e.T_Customer)
    .HasForeignKey(e => e.T_CustomerId);



            modelBuilder.Entity<T_Customer>()
.HasMany(e => e.T_ExchangeGifts)
.WithOptional(e => e.T_Customer)
.HasForeignKey(e => e.T_CustomerId);

            modelBuilder.Entity<T_Customer>()
.HasMany(e => e.T_CustomerAccept)
.WithOptional(e => e.T_Customer)
.HasForeignKey(e => e.T_CustomerId);

            modelBuilder.Entity<T_Customer>()
.HasMany(e => e.T_CustomerIntegralRecord)
.WithOptional(e => e.T_Customer)
.HasForeignKey(e => e.T_CustomerId);

            modelBuilder.Entity<T_Activity>()
.HasMany(e => e.T_CustomerActivity)
.WithOptional(e => e.T_Activity)
.HasForeignKey(e => e.T_ActivityId);

            modelBuilder.Entity<T_Activity>()
.HasMany(e => e.T_Reward)
.WithOptional(e => e.T_Activity)
.HasForeignKey(e => e.T_ActivityId);


            modelBuilder.Entity<T_Gifts>()
.HasMany(e => e.T_ExchangeGifts)
.WithOptional(e => e.T_Gifts)
.HasForeignKey(e => e.T_GiftsId);


            modelBuilder.Entity<T_Gifts>()
.HasMany(e => e.T_RewardChild)
.WithOptional(e => e.T_Gifts)
.HasForeignKey(e => e.T_GiftsId);

            modelBuilder.Entity<T_Gifts>()
.HasMany(e => e.T_GiftsChild)
.WithOptional(e => e.T_Gifts)
.HasForeignKey(e => e.T_GiftsId);



            modelBuilder.Entity<T_Reward>()
.HasMany(e => e.T_RewardChild)
.WithOptional(e => e.T_Reward)
.HasForeignKey(e => e.T_RewardId);
            modelBuilder.Entity<T_Reward>()
.HasMany(e => e.T_CustomerActivity)
.WithOptional(e => e.T_Reward)
.HasForeignKey(e => e.T_RewardId);



        }
    }
}
