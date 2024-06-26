﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GoodsExchange.data.Models;

public partial class Net1710_221_7_GoodsExchangeContext : DbContext
{
    public Net1710_221_7_GoodsExchangeContext(DbContextOptions<Net1710_221_7_GoodsExchangeContext> options)
        : base(options)
    {
    }

    public Net1710_221_7_GoodsExchangeContext()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("data source=(local);initial catalog=Net1710_221_7_GoodsExchange;user id=sa;password=12345;Integrated Security=True;TrustServerCertificate=True");
        base.OnConfiguring(optionsBuilder);
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Offer> Offers { get; set; }

    public virtual DbSet<OfferDetail> OfferDetails { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A2B9B9FF543");

            entity.ToTable("Category");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comment__C3B4DFAA535D87A9");

            entity.ToTable("Comment");

            entity.Property(e => e.CommentId).HasColumnName("CommentID");
            entity.Property(e => e.Content).HasMaxLength(2000);
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.DateTime).HasColumnType("datetime");
            entity.Property(e => e.PostId).HasColumnName("PostID");
            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasOne(d => d.Customer).WithMany(p => p.Comments)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comment__Custome__4222D4EF");

            entity.HasOne(d => d.Post).WithMany(p => p.Comments)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comment__PostID__412EB0B6");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customer");

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(500);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(250);
            entity.Property(e => e.Phone)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Offer>(entity =>
        {
            entity.HasKey(e => e.OfferId).HasName("PK_PostOffer");

            entity.ToTable("Offer");

            entity.Property(e => e.OfferId).HasColumnName("OfferID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

            entity.HasOne(d => d.Customer).WithMany(p => p.Offers)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Customer_Offer");
        });

        modelBuilder.Entity<OfferDetail>(entity =>
        {
            entity.HasKey(e => e.OfferDetailId).HasName("PK_Offers");

            entity.ToTable("OfferDetail");

            entity.Property(e => e.OfferDetailId).HasColumnName("OfferDetailID");
            entity.Property(e => e.Note).HasMaxLength(500);
            entity.Property(e => e.OfferId).HasColumnName("OfferID");
            entity.Property(e => e.PostId).HasColumnName("PostID");
            entity.Property(e => e.TraderItem)
                .IsRequired()
                .HasMaxLength(500);

            entity.HasOne(d => d.Offer).WithMany(p => p.OfferDetails)
                .HasForeignKey(d => d.OfferId)
                .HasConstraintName("FK_Offer_newKey");

            entity.HasOne(d => d.Post).WithMany(p => p.OfferDetails)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK_Post_OfferDetail");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("PK__Post__AA12603865CFA478");

            entity.ToTable("Post");

            entity.Property(e => e.PostId).HasColumnName("PostID");
            entity.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(500);
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(4000);
            entity.Property(e => e.ExchangeDate).HasColumnType("datetime");
            entity.Property(e => e.PostOwnerId).HasColumnName("PostOwnerID");
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasOne(d => d.Category).WithMany(p => p.Posts)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Post__CategoryID__45F365D3");

            entity.HasOne(d => d.PostOwner).WithMany(p => p.Posts)
                .HasForeignKey(d => d.PostOwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Post__PostOwnerI__46E78A0C");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}