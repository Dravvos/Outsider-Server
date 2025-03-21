﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Outsider.PedidoAPI.Model.Context;

#nullable disable

namespace Outsider.PedidoAPI.Migrations
{
    [DbContext(typeof(OutsiderContext))]
    [Migration("20250219232443_PedidoDB")]
    partial class PedidoDB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Outside.PedidoAPI.Model.EnderecoModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)")
                        .HasColumnName("Bairro");

                    b.Property<string>("CEP")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)")
                        .HasColumnName("CEP");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)")
                        .HasColumnName("Cidade");

                    b.Property<string>("Complemento")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("Complemento");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("datetime2")
                        .HasColumnName("DataAlteracao");

                    b.Property<DateTime>("DataInclusao")
                        .HasColumnType("datetime2")
                        .HasColumnName("DataInclusao");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)")
                        .HasColumnName("Estado");

                    b.Property<string>("Logradouro")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)")
                        .HasColumnName("Logradouro");

                    b.Property<int>("Numero")
                        .HasColumnType("int")
                        .HasColumnName("Numero");

                    b.Property<string>("Recebedor")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("Recebedor");

                    b.Property<Guid>("UsuarioId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UsuarioId");

                    b.HasKey("Id");

                    b.ToTable("Endereco");
                });

            modelBuilder.Entity("Outsider.PedidoAPI.Model.PedidoItemModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("datetime2")
                        .HasColumnName("DataAlteracao");

                    b.Property<DateTime>("DataInclusao")
                        .HasColumnType("datetime2")
                        .HasColumnName("DataInclusao");

                    b.Property<string>("NomeProduto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("NomeProduto");

                    b.Property<Guid>("PedidoId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("PedidoId");

                    b.Property<float>("Preco")
                        .HasColumnType("real")
                        .HasColumnName("Preco");

                    b.Property<Guid>("ProdutoId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ProdutoId");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int")
                        .HasColumnName("Quantidade");

                    b.HasKey("Id");

                    b.HasIndex("PedidoId");

                    b.HasIndex("ProdutoId");

                    b.ToTable("PedidoItem");
                });

            modelBuilder.Entity("Outsider.PedidoAPI.Model.PedidoModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<string>("CodigoCupom")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("CodigoCupom");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2")
                        .HasColumnName("DataCompra");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("datetime2")
                        .HasColumnName("DataAlteracao");

                    b.Property<DateTime>("DataInclusao")
                        .HasColumnType("datetime2")
                        .HasColumnName("DataInclusao");

                    b.Property<DateTime>("DataPedido")
                        .HasColumnType("datetime2")
                        .HasColumnName("DataPedido");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Email");

                    b.Property<Guid>("EnderecoId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("EnderecoId");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Nome");

                    b.Property<bool>("Pago")
                        .HasColumnType("bit")
                        .HasColumnName("PedidoPago");

                    b.Property<int>("QuantidadeItens")
                        .HasColumnType("int")
                        .HasColumnName("QuantidadeItens");

                    b.Property<string>("Sobrenome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Sobrenome");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Telefone");

                    b.Property<Guid>("UsuarioId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UsuarioId");

                    b.Property<float>("ValorCompra")
                        .HasColumnType("real")
                        .HasColumnName("ValorCompra");

                    b.Property<float>("ValorDesconto")
                        .HasColumnType("real")
                        .HasColumnName("ValorDesconto");

                    b.HasKey("Id");

                    b.HasIndex("EnderecoId");

                    b.ToTable("Pedido");
                });

            modelBuilder.Entity("Outsider.PedidoAPI.Model.ProdutoModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("datetime2")
                        .HasColumnName("DataAlteracao");

                    b.Property<DateTime>("DataInclusao")
                        .HasColumnType("datetime2")
                        .HasColumnName("DataInclusao");

                    b.Property<string>("Descricao")
                        .HasMaxLength(10000)
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Descricao");

                    b.Property<int>("Estoque")
                        .HasColumnType("int")
                        .HasColumnName("Estoque");

                    b.Property<Guid>("IdTGCategoria")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("IdTGCategoria");

                    b.Property<Guid>("IdTGCor")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("IdTGCor");

                    b.Property<Guid>("IdTGTamanho")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("IdTGTamanho");

                    b.Property<byte[]>("Imagem")
                        .IsRequired()
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("Imagem");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("Nome");

                    b.Property<float>("Peso")
                        .HasColumnType("real")
                        .HasColumnName("Peso");

                    b.Property<float>("Preco")
                        .HasColumnType("real")
                        .HasColumnName("Preco");

                    b.Property<string>("SKU")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("SKU");

                    b.HasKey("Id");

                    b.HasIndex("IdTGCategoria");

                    b.HasIndex("IdTGCor");

                    b.HasIndex("IdTGTamanho");

                    b.ToTable("Produto");
                });

            modelBuilder.Entity("Outsider.PedidoAPI.Model.TabelaGeralItemModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("datetime2")
                        .HasColumnName("DataAlteracao");

                    b.Property<DateTime>("DataInclusao")
                        .HasColumnType("datetime2")
                        .HasColumnName("DataInclusao");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("Descricao");

                    b.Property<string>("Sigla")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)")
                        .HasColumnName("Sigla");

                    b.Property<Guid>("TabelaGeralId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("TabelaGeralId");

                    b.HasKey("Id");

                    b.HasIndex("TabelaGeralId");

                    b.ToTable("TabelaGeralItem");
                });

            modelBuilder.Entity("Outsider.PedidoAPI.Model.TabelaGeralModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("datetime2")
                        .HasColumnName("DataAlteracao");

                    b.Property<DateTime>("DataInclusao")
                        .HasColumnType("datetime2")
                        .HasColumnName("DataInclusao");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("Descricao");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Nome");

                    b.HasKey("Id");

                    b.ToTable("TabelaGeral");
                });

            modelBuilder.Entity("Outsider.PedidoAPI.Model.PedidoItemModel", b =>
                {
                    b.HasOne("Outsider.PedidoAPI.Model.PedidoModel", "Pedido")
                        .WithMany("ItensPedido")
                        .HasForeignKey("PedidoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Outsider.PedidoAPI.Model.ProdutoModel", "Produto")
                        .WithMany()
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pedido");

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("Outsider.PedidoAPI.Model.PedidoModel", b =>
                {
                    b.HasOne("Outside.PedidoAPI.Model.EnderecoModel", "Endereco")
                        .WithMany()
                        .HasForeignKey("EnderecoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Endereco");
                });

            modelBuilder.Entity("Outsider.PedidoAPI.Model.ProdutoModel", b =>
                {
                    b.HasOne("Outsider.PedidoAPI.Model.TabelaGeralItemModel", "Categoria")
                        .WithMany()
                        .HasForeignKey("IdTGCategoria")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Outsider.PedidoAPI.Model.TabelaGeralItemModel", "Cor")
                        .WithMany()
                        .HasForeignKey("IdTGCor")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Outsider.PedidoAPI.Model.TabelaGeralItemModel", "Tamanho")
                        .WithMany()
                        .HasForeignKey("IdTGTamanho")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Categoria");

                    b.Navigation("Cor");

                    b.Navigation("Tamanho");
                });

            modelBuilder.Entity("Outsider.PedidoAPI.Model.TabelaGeralItemModel", b =>
                {
                    b.HasOne("Outsider.PedidoAPI.Model.TabelaGeralModel", "TabelaGeral")
                        .WithMany()
                        .HasForeignKey("TabelaGeralId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TabelaGeral");
                });

            modelBuilder.Entity("Outsider.PedidoAPI.Model.PedidoModel", b =>
                {
                    b.Navigation("ItensPedido");
                });
#pragma warning restore 612, 618
        }
    }
}
