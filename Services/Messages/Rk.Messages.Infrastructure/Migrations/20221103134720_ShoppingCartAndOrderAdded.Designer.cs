﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Rk.Messages.Infrastructure.EFCore;

#nullable disable

namespace Messages.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20221103134720_ShoppingCartAndOrderAdded")]
    partial class ShoppingCartAndOrderAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Rk.Messages.Domain.Entities.AttributeValue", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("AttributeId")
                        .HasColumnType("bigint")
                        .HasColumnName("attributeid");

                    b.Property<long>("BaseProductId")
                        .HasColumnType("bigint")
                        .HasColumnName("baseproductid");

                    b.Property<string>("Value")
                        .HasMaxLength(1024)
                        .HasColumnType("character varying(1024)")
                        .HasColumnName("value");

                    b.HasKey("Id")
                        .HasName("pk_attributevalues");

                    b.HasIndex("AttributeId")
                        .HasDatabaseName("ix_attributevalues_attributeid");

                    b.HasIndex("BaseProductId")
                        .HasDatabaseName("ix_attributevalues_baseproductid");

                    b.ToTable("attributevalues", (string)null);
                });

            modelBuilder.Entity("Rk.Messages.Domain.Entities.CatalogSection", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text")
                        .HasColumnName("createdby");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("lastmodified");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text")
                        .HasColumnName("lastmodifiedby");

                    b.Property<string>("Name")
                        .HasMaxLength(1024)
                        .HasColumnType("character varying(1024)")
                        .HasColumnName("name");

                    b.Property<long?>("ParentCatalogSectionId")
                        .HasColumnType("bigint")
                        .HasColumnName("parentcatalogsectionid");

                    b.HasKey("Id")
                        .HasName("pk_catalogsections");

                    b.HasIndex("ParentCatalogSectionId")
                        .HasDatabaseName("ix_catalogsections_parentcatalogsectionid");

                    b.ToTable("catalogsections", (string)null);
                });

            modelBuilder.Entity("Rk.Messages.Domain.Entities.Document", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<Guid>("FileId")
                        .HasColumnType("uuid")
                        .HasColumnName("fileid");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("filename");

                    b.HasKey("Id")
                        .HasName("pk_documents");

                    b.ToTable("documents", (string)null);
                });

            modelBuilder.Entity("Rk.Messages.Domain.Entities.Order", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Comments")
                        .HasMaxLength(1024)
                        .HasColumnType("character varying(1024)")
                        .HasColumnName("comments");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created");

                    b.Property<long>("OrganizationId")
                        .HasColumnType("bigint")
                        .HasColumnName("organizationid");

                    b.Property<string>("UserName")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("username");

                    b.HasKey("Id")
                        .HasName("pk_orders");

                    b.HasIndex("OrganizationId")
                        .HasDatabaseName("ix_orders_organizationid");

                    b.ToTable("orders", (string)null);
                });

            modelBuilder.Entity("Rk.Messages.Domain.Entities.OrderItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("OrderId")
                        .HasColumnType("bigint")
                        .HasColumnName("orderid");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric")
                        .HasColumnName("price");

                    b.Property<long>("ProductId")
                        .HasColumnType("bigint")
                        .HasColumnName("productid");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer")
                        .HasColumnName("quantity");

                    b.HasKey("Id")
                        .HasName("pk_orderitems");

                    b.HasIndex("OrderId")
                        .HasDatabaseName("ix_orderitems_orderid");

                    b.HasIndex("ProductId")
                        .HasDatabaseName("ix_orderitems_productid");

                    b.ToTable("orderitems", (string)null);
                });

            modelBuilder.Entity("Rk.Messages.Domain.Entities.Organization", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("FullName")
                        .HasMaxLength(1024)
                        .HasColumnType("character varying(1024)")
                        .HasColumnName("fullname");

                    b.Property<string>("Inn")
                        .HasMaxLength(12)
                        .HasColumnType("character varying(12)")
                        .HasColumnName("inn");

                    b.Property<string>("Kpp")
                        .HasMaxLength(9)
                        .HasColumnType("character varying(9)")
                        .HasColumnName("kpp");

                    b.Property<string>("Name")
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)")
                        .HasColumnName("name");

                    b.Property<string>("Ogrn")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("ogrn");

                    b.HasKey("Id")
                        .HasName("pk_organizations");

                    b.ToTable("organizations", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            FullName = "Ракетно-космический центр «Прогресс», Самара",
                            Inn = "6312139922",
                            Kpp = "631201001",
                            Name = "Прогресс",
                            Ogrn = "1146312005344"
                        },
                        new
                        {
                            Id = 2L,
                            FullName = "АКЦИОНЕРНОЕ ОБЩЕСТВО \"ЗЛАТОУСТОВСКИЙ МАШИНОСТРОИТЕЛЬНЫЙ ЗАВОД\"",
                            Inn = "7404052938",
                            Kpp = "631201001",
                            Name = "Златоустовский машиностроительный завод",
                            Ogrn = "1146312005344"
                        });
                });

            modelBuilder.Entity("Rk.Messages.Domain.Entities.ProductAttribute", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_attributes");

                    b.ToTable("attributes", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Name = "Вес"
                        },
                        new
                        {
                            Id = 2L,
                            Name = "Длина"
                        },
                        new
                        {
                            Id = 3L,
                            Name = "Ширина"
                        },
                        new
                        {
                            Id = 4L,
                            Name = "Цвет"
                        });
                });

            modelBuilder.Entity("Rk.Messages.Domain.Entities.ProductDocument", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("BaseProductId")
                        .HasColumnType("bigint")
                        .HasColumnName("baseproductid");

                    b.Property<long>("DocumentId")
                        .HasColumnType("bigint")
                        .HasColumnName("documentid");

                    b.HasKey("Id")
                        .HasName("pk_productdocuments");

                    b.HasIndex("BaseProductId")
                        .HasDatabaseName("ix_productdocuments_baseproductid");

                    b.HasIndex("DocumentId")
                        .HasDatabaseName("ix_productdocuments_documentid");

                    b.ToTable("productdocuments", (string)null);
                });

            modelBuilder.Entity("Rk.Messages.Domain.Entities.Products.BaseProduct", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("CatalogSectionId")
                        .HasColumnType("bigint")
                        .HasColumnName("catalogsectionid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text")
                        .HasColumnName("createdby");

                    b.Property<string>("Description")
                        .HasMaxLength(1024)
                        .HasColumnType("character varying(1024)")
                        .HasColumnName("description");

                    b.Property<int>("ItemType")
                        .HasColumnType("integer")
                        .HasColumnName("itemtype");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("lastmodified");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text")
                        .HasColumnName("lastmodifiedby");

                    b.Property<string>("Name")
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_baseproduct");

                    b.HasIndex("CatalogSectionId")
                        .HasDatabaseName("ix_baseproduct_catalogsectionid");

                    b.ToTable("baseproduct", (string)null);

                    b.HasDiscriminator<int>("ItemType");
                });

            modelBuilder.Entity("Rk.Messages.Domain.Entities.ShoppingCartItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric")
                        .HasColumnName("price");

                    b.Property<long>("ProductId")
                        .HasColumnType("bigint")
                        .HasColumnName("productid");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer")
                        .HasColumnName("quantity");

                    b.Property<string>("UserName")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("username");

                    b.HasKey("Id")
                        .HasName("pk_shoppingcartitems");

                    b.HasIndex("ProductId")
                        .HasDatabaseName("ix_shoppingcartitems_productid");

                    b.ToTable("shoppingcartitems", (string)null);
                });

            modelBuilder.Entity("Rk.Messages.Domain.Entities.Products.Product", b =>
                {
                    b.HasBaseType("Rk.Messages.Domain.Entities.Products.BaseProduct");

                    b.Property<string>("CodeTnVed")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("codetnved");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric")
                        .HasColumnName("price");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("Rk.Messages.Domain.Entities.Products.ServiceProduct", b =>
                {
                    b.HasBaseType("Rk.Messages.Domain.Entities.Products.BaseProduct");

                    b.HasDiscriminator().HasValue(2);
                });

            modelBuilder.Entity("Rk.Messages.Domain.Entities.Products.Technology", b =>
                {
                    b.HasBaseType("Rk.Messages.Domain.Entities.Products.BaseProduct");

                    b.HasDiscriminator().HasValue(3);
                });

            modelBuilder.Entity("Rk.Messages.Domain.Entities.AttributeValue", b =>
                {
                    b.HasOne("Rk.Messages.Domain.Entities.ProductAttribute", "Attribute")
                        .WithMany()
                        .HasForeignKey("AttributeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_attributevalues_attributes_attributeid");

                    b.HasOne("Rk.Messages.Domain.Entities.Products.BaseProduct", "BaseProduct")
                        .WithMany("AttributeValues")
                        .HasForeignKey("BaseProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_attributevalues_baseproduct_baseproductid");

                    b.Navigation("Attribute");

                    b.Navigation("BaseProduct");
                });

            modelBuilder.Entity("Rk.Messages.Domain.Entities.CatalogSection", b =>
                {
                    b.HasOne("Rk.Messages.Domain.Entities.CatalogSection", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentCatalogSectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_catalogsections_catalogsections_catalogsectionid");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("Rk.Messages.Domain.Entities.Order", b =>
                {
                    b.HasOne("Rk.Messages.Domain.Entities.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_orders_organizations_organizationid");

                    b.Navigation("Organization");
                });

            modelBuilder.Entity("Rk.Messages.Domain.Entities.OrderItem", b =>
                {
                    b.HasOne("Rk.Messages.Domain.Entities.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_orderitems_orders_orderid");

                    b.HasOne("Rk.Messages.Domain.Entities.Products.BaseProduct", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_orderitems_baseproduct_productid");

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Rk.Messages.Domain.Entities.ProductDocument", b =>
                {
                    b.HasOne("Rk.Messages.Domain.Entities.Products.BaseProduct", "BaseProduct")
                        .WithMany("ProductDocuments")
                        .HasForeignKey("BaseProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_productdocuments_baseproduct_baseproductid");

                    b.HasOne("Rk.Messages.Domain.Entities.Document", "Document")
                        .WithMany()
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_productdocuments_documents_documentid");

                    b.Navigation("BaseProduct");

                    b.Navigation("Document");
                });

            modelBuilder.Entity("Rk.Messages.Domain.Entities.Products.BaseProduct", b =>
                {
                    b.HasOne("Rk.Messages.Domain.Entities.CatalogSection", "CatalogSection")
                        .WithMany("Products")
                        .HasForeignKey("CatalogSectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_baseproduct_catalogsections_catalogsectionid");

                    b.Navigation("CatalogSection");
                });

            modelBuilder.Entity("Rk.Messages.Domain.Entities.ShoppingCartItem", b =>
                {
                    b.HasOne("Rk.Messages.Domain.Entities.Products.BaseProduct", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_shoppingcartitems_baseproduct_productid");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Rk.Messages.Domain.Entities.CatalogSection", b =>
                {
                    b.Navigation("Children");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("Rk.Messages.Domain.Entities.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("Rk.Messages.Domain.Entities.Products.BaseProduct", b =>
                {
                    b.Navigation("AttributeValues");

                    b.Navigation("ProductDocuments");
                });
#pragma warning restore 612, 618
        }
    }
}
