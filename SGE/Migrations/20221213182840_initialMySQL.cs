using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SGE.Migrations
{
    public partial class initialMySQL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Banco",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 100, nullable: true),
                    Codigo = table.Column<string>(maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banco", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cidade",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdEstado = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cidade", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DespesaViagem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    UsuarioCadastro = table.Column<int>(nullable: true),
                    DataAlteracao = table.Column<DateTime>(nullable: true),
                    UsuarioAlteracao = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Almoco = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Jantar = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Lanche = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Estadia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CoeficienteKM = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CoeficienteNF = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DespesaViagem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Empresa",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RazaoSocial = table.Column<string>(maxLength: 100, nullable: false),
                    NomeFantasia = table.Column<string>(maxLength: 100, nullable: true),
                    CNPJ = table.Column<string>(maxLength: 18, nullable: false),
                    Token = table.Column<string>(maxLength: 36, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Telefone = table.Column<string>(maxLength: 15, nullable: true),
                    Celular = table.Column<string>(maxLength: 15, nullable: true),
                    Email = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Estado",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdPais = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(maxLength: 100, nullable: true),
                    Sigla = table.Column<string>(maxLength: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estado", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FormaPagamento",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    UsuarioCadastro = table.Column<int>(nullable: true),
                    DataAlteracao = table.Column<DateTime>(nullable: true),
                    UsuarioAlteracao = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(maxLength: 100, nullable: true),
                    Descricao = table.Column<string>(maxLength: 500, nullable: true),
                    MeioPagamento = table.Column<int>(nullable: false),
                    QuantidadeParcelas = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormaPagamento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Instrumento",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    UsuarioCadastro = table.Column<int>(nullable: true),
                    DataAlteracao = table.Column<DateTime>(nullable: true),
                    UsuarioAlteracao = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(maxLength: 100, nullable: true),
                    Descricao = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instrumento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModeloContrato",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    UsuarioCadastro = table.Column<int>(nullable: true),
                    DataAlteracao = table.Column<DateTime>(nullable: true),
                    UsuarioAlteracao = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(maxLength: 100, nullable: true),
                    Titulo = table.Column<string>(maxLength: 500, nullable: true),
                    TextoInicial = table.Column<string>(maxLength: 1000, nullable: true),
                    TextoInicialPosContratante = table.Column<string>(maxLength: 1000, nullable: true),
                    ClausulaPrimeira = table.Column<string>(maxLength: 4000, nullable: true),
                    ClausulaSegunda = table.Column<string>(maxLength: 4000, nullable: true),
                    ClausulaTerceira = table.Column<string>(maxLength: 4000, nullable: true),
                    ClausulaQuarta = table.Column<string>(maxLength: 4000, nullable: true),
                    ClausulaQuinta = table.Column<string>(maxLength: 4000, nullable: true),
                    ClausulasFinais = table.Column<string>(maxLength: 10000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloContrato", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModeloOrcamento",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    UsuarioCadastro = table.Column<int>(nullable: true),
                    DataAlteracao = table.Column<DateTime>(nullable: true),
                    UsuarioAlteracao = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(maxLength: 100, nullable: true),
                    TextoInicial = table.Column<string>(maxLength: 1000, nullable: true),
                    TextoFinal = table.Column<string>(maxLength: 4000, nullable: true),
                    Assinatura = table.Column<string>(maxLength: 1000, nullable: true),
                    Rodapé = table.Column<string>(maxLength: 1000, nullable: true),
                    Logomarca = table.Column<byte[]>(nullable: true),
                    ContentType = table.Column<string>(maxLength: 50, nullable: true),
                    RazaoEmpresa = table.Column<string>(maxLength: 200, nullable: true),
                    CNPJ = table.Column<string>(maxLength: 20, nullable: true),
                    LogomarcaPath = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloOrcamento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Montagem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    UsuarioCadastro = table.Column<int>(nullable: true),
                    DataAlteracao = table.Column<DateTime>(nullable: true),
                    UsuarioAlteracao = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Montagem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pais",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pais", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlanoContas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    UsuarioCadastro = table.Column<int>(nullable: true),
                    DataAlteracao = table.Column<DateTime>(nullable: true),
                    UsuarioAlteracao = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    TipoMovimentacao = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanoContas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PosicaoAlmoxarifado",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    UsuarioCadastro = table.Column<int>(nullable: true),
                    DataAlteracao = table.Column<DateTime>(nullable: true),
                    UsuarioAlteracao = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(maxLength: 100, nullable: false),
                    Observacao = table.Column<string>(maxLength: 3000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PosicaoAlmoxarifado", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    UsuarioCadastro = table.Column<int>(nullable: true),
                    DataAlteracao = table.Column<DateTime>(nullable: true),
                    UsuarioAlteracao = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(maxLength: 100, nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    NomeUsuario = table.Column<string>(maxLength: 10, nullable: false),
                    Senha = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContaCorrente",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    UsuarioCadastro = table.Column<int>(nullable: true),
                    DataAlteracao = table.Column<DateTime>(nullable: true),
                    UsuarioAlteracao = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    numero = table.Column<string>(maxLength: 20, nullable: true),
                    nome = table.Column<string>(maxLength: 30, nullable: true),
                    agencia = table.Column<string>(maxLength: 10, nullable: true),
                    BancoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContaCorrente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContaCorrente_Banco_BancoId",
                        column: x => x.BancoId,
                        principalTable: "Banco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Entidade",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    UsuarioCadastro = table.Column<int>(nullable: true),
                    DataAlteracao = table.Column<DateTime>(nullable: true),
                    UsuarioAlteracao = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(maxLength: 100, nullable: false),
                    NomeFantasia = table.Column<string>(maxLength: 100, nullable: true),
                    TipoPessoa = table.Column<int>(nullable: false),
                    CPFCNPJ = table.Column<string>(maxLength: 20, nullable: false),
                    RGInscricaoEstadual = table.Column<string>(maxLength: 30, nullable: true),
                    CEP = table.Column<string>(maxLength: 10, nullable: true),
                    Logradouro = table.Column<string>(maxLength: 100, nullable: true),
                    Numero = table.Column<string>(maxLength: 10, nullable: true),
                    Complemento = table.Column<string>(maxLength: 50, nullable: true),
                    Bairro = table.Column<string>(maxLength: 50, nullable: true),
                    CidadeId = table.Column<int>(nullable: true),
                    EstadoId = table.Column<int>(nullable: true),
                    PaisId = table.Column<int>(nullable: true),
                    Telefone1 = table.Column<string>(maxLength: 20, nullable: true),
                    Telefone2 = table.Column<string>(maxLength: 20, nullable: true),
                    Celular = table.Column<string>(maxLength: 20, nullable: true),
                    Email = table.Column<string>(maxLength: 200, nullable: true),
                    WebSite = table.Column<string>(maxLength: 200, nullable: true),
                    Observacoes = table.Column<string>(maxLength: 500, nullable: true),
                    Cliente = table.Column<bool>(nullable: false),
                    Fornecedor = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entidade", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entidade_Cidade_CidadeId",
                        column: x => x.CidadeId,
                        principalTable: "Cidade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Entidade_Estado_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Entidade_Pais_PaisId",
                        column: x => x.PaisId,
                        principalTable: "Pais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Funcionario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    UsuarioCadastro = table.Column<int>(nullable: true),
                    DataAlteracao = table.Column<DateTime>(nullable: true),
                    UsuarioAlteracao = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(maxLength: 200, nullable: true),
                    Tratamento = table.Column<string>(maxLength: 100, nullable: true),
                    Cargo = table.Column<int>(nullable: false),
                    EstadoCivil = table.Column<int>(nullable: false),
                    Escolaridade = table.Column<string>(maxLength: 100, nullable: true),
                    DataNascimento = table.Column<DateTime>(nullable: true),
                    NomePai = table.Column<string>(maxLength: 200, nullable: true),
                    NomeMae = table.Column<string>(maxLength: 200, nullable: true),
                    EstadoNascId = table.Column<int>(nullable: true),
                    CidadeNascId = table.Column<int>(nullable: true),
                    CEP = table.Column<string>(maxLength: 10, nullable: true),
                    Logradouro = table.Column<string>(maxLength: 100, nullable: true),
                    Numero = table.Column<string>(maxLength: 10, nullable: true),
                    Complemento = table.Column<string>(maxLength: 50, nullable: true),
                    Bairro = table.Column<string>(maxLength: 50, nullable: true),
                    CidadeId = table.Column<int>(nullable: true),
                    EstadoId = table.Column<int>(nullable: true),
                    PaisId = table.Column<int>(nullable: true),
                    Telefone1 = table.Column<string>(maxLength: 20, nullable: true),
                    Telefone2 = table.Column<string>(maxLength: 20, nullable: true),
                    Celular = table.Column<string>(maxLength: 20, nullable: true),
                    Email = table.Column<string>(maxLength: 200, nullable: true),
                    DataAdmissao = table.Column<DateTime>(nullable: true),
                    DataDemissao = table.Column<DateTime>(nullable: true),
                    CPF = table.Column<string>(maxLength: 20, nullable: true),
                    RG = table.Column<string>(maxLength: 20, nullable: true),
                    OrgaoEmissor = table.Column<string>(maxLength: 20, nullable: true),
                    CTPS = table.Column<string>(maxLength: 20, nullable: true),
                    CNH = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Funcionario_Cidade_CidadeId",
                        column: x => x.CidadeId,
                        principalTable: "Cidade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Funcionario_Cidade_CidadeNascId",
                        column: x => x.CidadeNascId,
                        principalTable: "Cidade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Funcionario_Estado_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Funcionario_Estado_EstadoNascId",
                        column: x => x.EstadoNascId,
                        principalTable: "Estado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Funcionario_Pais_PaisId",
                        column: x => x.PaisId,
                        principalTable: "Pais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LocalEvento",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    UsuarioCadastro = table.Column<int>(nullable: true),
                    DataAlteracao = table.Column<DateTime>(nullable: true),
                    UsuarioAlteracao = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(maxLength: 100, nullable: true),
                    Descricao = table.Column<string>(maxLength: 500, nullable: true),
                    Observacoes = table.Column<string>(maxLength: 1000, nullable: true),
                    Contato = table.Column<string>(maxLength: 100, nullable: true),
                    PontoReferencia = table.Column<string>(maxLength: 100, nullable: true),
                    Capacidade = table.Column<int>(nullable: false),
                    CEP = table.Column<string>(maxLength: 10, nullable: true),
                    Logradouro = table.Column<string>(maxLength: 100, nullable: true),
                    Numero = table.Column<string>(maxLength: 10, nullable: true),
                    Complemento = table.Column<string>(maxLength: 50, nullable: true),
                    Bairro = table.Column<string>(maxLength: 50, nullable: true),
                    CidadeId = table.Column<int>(nullable: true),
                    EstadoId = table.Column<int>(nullable: true),
                    PaisId = table.Column<int>(nullable: true),
                    Telefone1 = table.Column<string>(maxLength: 20, nullable: true),
                    Telefone2 = table.Column<string>(maxLength: 20, nullable: true),
                    Celular = table.Column<string>(maxLength: 20, nullable: true),
                    Email = table.Column<string>(maxLength: 200, nullable: true),
                    WebSite = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalEvento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocalEvento_Cidade_CidadeId",
                        column: x => x.CidadeId,
                        principalTable: "Cidade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LocalEvento_Estado_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LocalEvento_Pais_PaisId",
                        column: x => x.PaisId,
                        principalTable: "Pais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Endereco",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    UsuarioCadastro = table.Column<int>(nullable: true),
                    DataAlteracao = table.Column<DateTime>(nullable: true),
                    UsuarioAlteracao = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    EntidadeId = table.Column<int>(nullable: true),
                    Nome = table.Column<string>(maxLength: 50, nullable: true),
                    CEP = table.Column<string>(maxLength: 10, nullable: true),
                    Logradouro = table.Column<string>(maxLength: 100, nullable: true),
                    Numero = table.Column<string>(maxLength: 10, nullable: true),
                    Complemento = table.Column<string>(maxLength: 50, nullable: true),
                    Bairro = table.Column<string>(maxLength: 50, nullable: true),
                    CidadeId = table.Column<int>(nullable: true),
                    EstadoId = table.Column<int>(nullable: true),
                    PaisId = table.Column<int>(nullable: true),
                    Telefone1 = table.Column<string>(maxLength: 20, nullable: true),
                    Telefone2 = table.Column<string>(maxLength: 20, nullable: true),
                    Celular = table.Column<string>(maxLength: 20, nullable: true),
                    Email = table.Column<string>(maxLength: 200, nullable: true),
                    WebSite = table.Column<string>(maxLength: 200, nullable: true),
                    Principal = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereco", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Endereco_Cidade_CidadeId",
                        column: x => x.CidadeId,
                        principalTable: "Cidade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Endereco_Entidade_EntidadeId",
                        column: x => x.EntidadeId,
                        principalTable: "Entidade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Endereco_Estado_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Endereco_Pais_PaisId",
                        column: x => x.PaisId,
                        principalTable: "Pais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sistema",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    UsuarioCadastro = table.Column<int>(nullable: true),
                    DataAlteracao = table.Column<DateTime>(nullable: true),
                    UsuarioAlteracao = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(maxLength: 3000, nullable: true),
                    FornecedorId = table.Column<int>(nullable: true),
                    InstrumentoId = table.Column<int>(nullable: true),
                    Codigo = table.Column<string>(maxLength: 10, nullable: true),
                    Imagem = table.Column<string>(maxLength: 500, nullable: true),
                    NumeroSerie = table.Column<string>(maxLength: 50, nullable: true),
                    QuantidadeEstoque = table.Column<int>(nullable: false),
                    ValorSistema = table.Column<decimal>(nullable: true),
                    ValorLocacao = table.Column<decimal>(nullable: true),
                    DataAquisição = table.Column<DateTime>(nullable: true),
                    DataBaixa = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sistema", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sistema_Entidade_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Entidade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sistema_Instrumento_InstrumentoId",
                        column: x => x.InstrumentoId,
                        principalTable: "Instrumento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Almoxarifado",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    UsuarioCadastro = table.Column<int>(nullable: true),
                    DataAlteracao = table.Column<DateTime>(nullable: true),
                    UsuarioAlteracao = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(maxLength: 3000, nullable: true),
                    QuantidadeEstoque = table.Column<int>(nullable: false),
                    LocalEventoId = table.Column<int>(nullable: true),
                    PosicaoAlmoxarifadoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Almoxarifado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Almoxarifado_LocalEvento_LocalEventoId",
                        column: x => x.LocalEventoId,
                        principalTable: "LocalEvento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Almoxarifado_PosicaoAlmoxarifado_PosicaoAlmoxarifadoId",
                        column: x => x.PosicaoAlmoxarifadoId,
                        principalTable: "PosicaoAlmoxarifado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orcamento",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    UsuarioCadastro = table.Column<int>(nullable: true),
                    DataAlteracao = table.Column<DateTime>(nullable: true),
                    UsuarioAlteracao = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    ModeloOrcamentoId = table.Column<int>(nullable: true),
                    NumeroProposta = table.Column<string>(maxLength: 10, nullable: true),
                    NomeEvento = table.Column<string>(maxLength: 100, nullable: false),
                    LocalEventoId = table.Column<int>(nullable: true),
                    SituacaoOrcamento = table.Column<int>(nullable: false),
                    dataProposta = table.Column<DateTime>(nullable: false),
                    dataInicio = table.Column<DateTime>(nullable: false),
                    dataFim = table.Column<DateTime>(nullable: false),
                    dataMontagem = table.Column<DateTime>(nullable: true),
                    TipoProposta = table.Column<int>(nullable: false),
                    EntidadeId = table.Column<int>(nullable: true),
                    NomeSolicitante = table.Column<string>(maxLength: 200, nullable: true),
                    EmpresaSolicitante = table.Column<string>(maxLength: 200, nullable: true),
                    Telefone = table.Column<string>(maxLength: 20, nullable: true),
                    Email = table.Column<string>(maxLength: 200, nullable: true),
                    EstadoId = table.Column<int>(nullable: true),
                    CidadeId = table.Column<int>(nullable: true),
                    MostrarValoresUnitarios = table.Column<bool>(nullable: false),
                    MostrarValorTotal = table.Column<bool>(nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorDesconto = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CondicoesPagamento = table.Column<string>(maxLength: 1000, nullable: true),
                    Entrega = table.Column<string>(maxLength: 200, nullable: true),
                    ValidadeProposta = table.Column<string>(maxLength: 1000, nullable: true),
                    Observacoes = table.Column<string>(maxLength: 500, nullable: true),
                    ObservacoesChecklist = table.Column<string>(maxLength: 500, nullable: true),
                    GeradoEvento = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orcamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orcamento_Cidade_CidadeId",
                        column: x => x.CidadeId,
                        principalTable: "Cidade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orcamento_Entidade_EntidadeId",
                        column: x => x.EntidadeId,
                        principalTable: "Entidade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orcamento_Estado_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orcamento_LocalEvento_LocalEventoId",
                        column: x => x.LocalEventoId,
                        principalTable: "LocalEvento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orcamento_ModeloOrcamento_ModeloOrcamentoId",
                        column: x => x.ModeloOrcamentoId,
                        principalTable: "ModeloOrcamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Case",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    UsuarioCadastro = table.Column<int>(nullable: true),
                    DataAlteracao = table.Column<DateTime>(nullable: true),
                    UsuarioAlteracao = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(maxLength: 3000, nullable: true),
                    Codigo = table.Column<string>(maxLength: 10, nullable: true),
                    SistemaId = table.Column<int>(nullable: true),
                    ValorLocacao = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Case", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Case_Sistema_SistemaId",
                        column: x => x.SistemaId,
                        principalTable: "Sistema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Evento",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    UsuarioCadastro = table.Column<int>(nullable: true),
                    DataAlteracao = table.Column<DateTime>(nullable: true),
                    UsuarioAlteracao = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    OrcamentoId = table.Column<int>(nullable: false),
                    DataAprovacao = table.Column<DateTime>(nullable: false),
                    NumeroOS = table.Column<string>(maxLength: 10, nullable: true),
                    NumeroProposta = table.Column<string>(maxLength: 10, nullable: true),
                    NomeEvento = table.Column<string>(maxLength: 100, nullable: false),
                    PontoReferencia = table.Column<string>(maxLength: 100, nullable: true),
                    LocalEventoId = table.Column<int>(nullable: true),
                    SituacaoEvento = table.Column<int>(nullable: false),
                    dataProposta = table.Column<DateTime>(nullable: false),
                    dataInicio = table.Column<DateTime>(nullable: false),
                    dataFim = table.Column<DateTime>(nullable: false),
                    dataMontagem = table.Column<DateTime>(nullable: true),
                    dataDesmontagem = table.Column<DateTime>(nullable: true),
                    TipoProposta = table.Column<int>(nullable: false),
                    EntidadeId = table.Column<int>(nullable: true),
                    NomeSolicitante = table.Column<string>(maxLength: 200, nullable: true),
                    EmpresaSolicitante = table.Column<string>(maxLength: 200, nullable: true),
                    Telefone = table.Column<string>(maxLength: 20, nullable: true),
                    Email = table.Column<string>(maxLength: 200, nullable: true),
                    EstadoId = table.Column<int>(nullable: true),
                    CidadeId = table.Column<int>(nullable: true),
                    MostrarValoresUnitarios = table.Column<bool>(nullable: false),
                    MostrarValorTotal = table.Column<bool>(nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorDesconto = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CondicoesPagamento = table.Column<string>(maxLength: 1000, nullable: true),
                    Entrega = table.Column<string>(maxLength: 200, nullable: true),
                    ValidadeProposta = table.Column<string>(maxLength: 1000, nullable: true),
                    Observacoes = table.Column<string>(maxLength: 500, nullable: true),
                    ObservacoesChecklist = table.Column<string>(maxLength: 500, nullable: true),
                    FuncionarioId = table.Column<int>(nullable: true),
                    GeradoContrato = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Evento_Cidade_CidadeId",
                        column: x => x.CidadeId,
                        principalTable: "Cidade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evento_Entidade_EntidadeId",
                        column: x => x.EntidadeId,
                        principalTable: "Entidade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evento_Estado_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evento_Funcionario_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evento_LocalEvento_LocalEventoId",
                        column: x => x.LocalEventoId,
                        principalTable: "LocalEvento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evento_Orcamento_OrcamentoId",
                        column: x => x.OrcamentoId,
                        principalTable: "Orcamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Equipamento",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    UsuarioCadastro = table.Column<int>(nullable: true),
                    DataAlteracao = table.Column<DateTime>(nullable: true),
                    UsuarioAlteracao = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(maxLength: 3000, nullable: true),
                    FornecedorId = table.Column<int>(nullable: false),
                    CaseId = table.Column<int>(nullable: true),
                    InstrumentoId = table.Column<int>(nullable: false),
                    Codigo = table.Column<string>(maxLength: 10, nullable: true),
                    Imagem = table.Column<string>(maxLength: 500, nullable: true),
                    NumeroSerie = table.Column<string>(maxLength: 50, nullable: true),
                    QuantidadeEstoque = table.Column<int>(nullable: false),
                    ValorSistema = table.Column<decimal>(nullable: true),
                    ValorLocacao = table.Column<decimal>(nullable: true),
                    DataAquisição = table.Column<DateTime>(nullable: true),
                    DataBaixa = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Equipamento_Case_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Case",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Equipamento_Entidade_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Entidade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Equipamento_Instrumento_InstrumentoId",
                        column: x => x.InstrumentoId,
                        principalTable: "Instrumento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SistemaCases",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SistemaId = table.Column<int>(nullable: true),
                    CaseId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SistemaCases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SistemaCases_Case_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Case",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SistemaCases_Sistema_SistemaId",
                        column: x => x.SistemaId,
                        principalTable: "Sistema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contrato",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    UsuarioCadastro = table.Column<int>(nullable: true),
                    DataAlteracao = table.Column<DateTime>(nullable: true),
                    UsuarioAlteracao = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    ModeloContratoId = table.Column<int>(nullable: true),
                    EventoId = table.Column<int>(nullable: false),
                    EntidadeId = table.Column<int>(nullable: true),
                    Numero = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contrato", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contrato_Entidade_EntidadeId",
                        column: x => x.EntidadeId,
                        principalTable: "Entidade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contrato_Evento_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Evento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contrato_ModeloContrato_ModeloContratoId",
                        column: x => x.ModeloContratoId,
                        principalTable: "ModeloContrato",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DespesaEvento",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    UsuarioCadastro = table.Column<int>(nullable: true),
                    DataAlteracao = table.Column<DateTime>(nullable: true),
                    UsuarioAlteracao = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    EventoId = table.Column<int>(nullable: false),
                    FuncionarioId = table.Column<int>(nullable: false),
                    MontagemId = table.Column<int>(nullable: false),
                    Almoco = table.Column<bool>(nullable: false),
                    Jantar = table.Column<bool>(nullable: false),
                    Lanche = table.Column<bool>(nullable: false),
                    Estadia = table.Column<bool>(nullable: false),
                    ValorAlmoco = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorJantar = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorLanche = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorEstadia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorDiversos = table.Column<decimal>(nullable: false),
                    ValorTotal = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DespesaEvento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DespesaEvento_Evento_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Evento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DespesaEvento_Funcionario_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DespesaEvento_Montagem_MontagemId",
                        column: x => x.MontagemId,
                        principalTable: "Montagem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovimentoFinanceiro",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    UsuarioCadastro = table.Column<int>(nullable: true),
                    DataAlteracao = table.Column<DateTime>(nullable: true),
                    UsuarioAlteracao = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    FormaPagamentoId = table.Column<int>(nullable: false),
                    PlanoContasId = table.Column<int>(nullable: false),
                    ContaCorrenteId = table.Column<int>(nullable: true),
                    EventoId = table.Column<int>(nullable: true),
                    EntidadeId = table.Column<int>(nullable: true),
                    NumeroDocumento = table.Column<string>(maxLength: 500, nullable: true),
                    Descricao = table.Column<string>(maxLength: 100, nullable: true),
                    Observacoes = table.Column<string>(maxLength: 500, nullable: true),
                    dataVencimento = table.Column<DateTime>(nullable: false),
                    dataPagamento = table.Column<DateTime>(nullable: true),
                    Pago = table.Column<bool>(nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TipoMovimentacao = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimentoFinanceiro", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovimentoFinanceiro_ContaCorrente_ContaCorrenteId",
                        column: x => x.ContaCorrenteId,
                        principalTable: "ContaCorrente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MovimentoFinanceiro_Entidade_EntidadeId",
                        column: x => x.EntidadeId,
                        principalTable: "Entidade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MovimentoFinanceiro_Evento_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Evento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MovimentoFinanceiro_FormaPagamento_FormaPagamentoId",
                        column: x => x.FormaPagamentoId,
                        principalTable: "FormaPagamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovimentoFinanceiro_PlanoContas_PlanoContasId",
                        column: x => x.PlanoContasId,
                        principalTable: "PlanoContas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CaseEquipamentos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CaseId = table.Column<int>(nullable: true),
                    EquipamentoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseEquipamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CaseEquipamentos_Case_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Case",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CaseEquipamentos_Equipamento_EquipamentoId",
                        column: x => x.EquipamentoId,
                        principalTable: "Equipamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EventoItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EventoId = table.Column<int>(nullable: false),
                    CaseId = table.Column<int>(nullable: true),
                    EquipamentoId = table.Column<int>(nullable: true),
                    SistemaId = table.Column<int>(nullable: true),
                    InstrumentoId = table.Column<int>(nullable: true),
                    NomeInstrumento = table.Column<string>(maxLength: 100, nullable: true),
                    NomeSistema = table.Column<string>(maxLength: 100, nullable: true),
                    Descricao = table.Column<string>(maxLength: 1000, nullable: true),
                    Quantidade = table.Column<int>(nullable: false),
                    Valor = table.Column<decimal>(nullable: false),
                    Diarias = table.Column<int>(nullable: false),
                    ValorTotalItem = table.Column<decimal>(nullable: false),
                    ValorSublocacao = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventoItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventoItem_Case_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Case",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventoItem_Equipamento_EquipamentoId",
                        column: x => x.EquipamentoId,
                        principalTable: "Equipamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventoItem_Evento_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Evento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventoItem_Instrumento_InstrumentoId",
                        column: x => x.InstrumentoId,
                        principalTable: "Instrumento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventoItem_Sistema_SistemaId",
                        column: x => x.SistemaId,
                        principalTable: "Sistema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrcamentoItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OrcamentoId = table.Column<int>(nullable: false),
                    CaseId = table.Column<int>(nullable: true),
                    EquipamentoId = table.Column<int>(nullable: true),
                    SistemaId = table.Column<int>(nullable: true),
                    InstrumentoId = table.Column<int>(nullable: true),
                    NomeInstrumento = table.Column<string>(maxLength: 100, nullable: true),
                    NomeSistema = table.Column<string>(maxLength: 100, nullable: true),
                    Descricao = table.Column<string>(maxLength: 1000, nullable: true),
                    Quantidade = table.Column<int>(nullable: false),
                    Valor = table.Column<decimal>(nullable: false),
                    Diarias = table.Column<int>(nullable: false),
                    ValorTotalItem = table.Column<decimal>(nullable: false),
                    ValorSublocacao = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrcamentoItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrcamentoItem_Case_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Case",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrcamentoItem_Equipamento_EquipamentoId",
                        column: x => x.EquipamentoId,
                        principalTable: "Equipamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrcamentoItem_Instrumento_InstrumentoId",
                        column: x => x.InstrumentoId,
                        principalTable: "Instrumento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrcamentoItem_Orcamento_OrcamentoId",
                        column: x => x.OrcamentoId,
                        principalTable: "Orcamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrcamentoItem_Sistema_SistemaId",
                        column: x => x.SistemaId,
                        principalTable: "Sistema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TabelaPreco",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    UsuarioCadastro = table.Column<int>(nullable: true),
                    DataAlteracao = table.Column<DateTime>(nullable: true),
                    UsuarioAlteracao = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Tabela = table.Column<short>(nullable: false),
                    Nome = table.Column<string>(maxLength: 100, nullable: true),
                    SistemaId = table.Column<int>(nullable: true),
                    CaseId = table.Column<int>(nullable: true),
                    EquipamentoId = table.Column<int>(nullable: true),
                    ValorCompra = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ValorLocacao = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TabelaPreco", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TabelaPreco_Case_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Case",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TabelaPreco_Equipamento_EquipamentoId",
                        column: x => x.EquipamentoId,
                        principalTable: "Equipamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TabelaPreco_Sistema_SistemaId",
                        column: x => x.SistemaId,
                        principalTable: "Sistema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Almoxarifado_LocalEventoId",
                table: "Almoxarifado",
                column: "LocalEventoId");

            migrationBuilder.CreateIndex(
                name: "IX_Almoxarifado_PosicaoAlmoxarifadoId",
                table: "Almoxarifado",
                column: "PosicaoAlmoxarifadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Case_SistemaId",
                table: "Case",
                column: "SistemaId");

            migrationBuilder.CreateIndex(
                name: "IX_CaseEquipamentos_CaseId",
                table: "CaseEquipamentos",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_CaseEquipamentos_EquipamentoId",
                table: "CaseEquipamentos",
                column: "EquipamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_ContaCorrente_BancoId",
                table: "ContaCorrente",
                column: "BancoId");

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_EntidadeId",
                table: "Contrato",
                column: "EntidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_EventoId",
                table: "Contrato",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_ModeloContratoId",
                table: "Contrato",
                column: "ModeloContratoId");

            migrationBuilder.CreateIndex(
                name: "IX_DespesaEvento_EventoId",
                table: "DespesaEvento",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_DespesaEvento_FuncionarioId",
                table: "DespesaEvento",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_DespesaEvento_MontagemId",
                table: "DespesaEvento",
                column: "MontagemId");

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_CidadeId",
                table: "Endereco",
                column: "CidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_EntidadeId",
                table: "Endereco",
                column: "EntidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_EstadoId",
                table: "Endereco",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_PaisId",
                table: "Endereco",
                column: "PaisId");

            migrationBuilder.CreateIndex(
                name: "IX_Entidade_CidadeId",
                table: "Entidade",
                column: "CidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_Entidade_EstadoId",
                table: "Entidade",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Entidade_PaisId",
                table: "Entidade",
                column: "PaisId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipamento_CaseId",
                table: "Equipamento",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipamento_FornecedorId",
                table: "Equipamento",
                column: "FornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipamento_InstrumentoId",
                table: "Equipamento",
                column: "InstrumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Evento_CidadeId",
                table: "Evento",
                column: "CidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_Evento_EntidadeId",
                table: "Evento",
                column: "EntidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_Evento_EstadoId",
                table: "Evento",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Evento_FuncionarioId",
                table: "Evento",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Evento_LocalEventoId",
                table: "Evento",
                column: "LocalEventoId");

            migrationBuilder.CreateIndex(
                name: "IX_Evento_OrcamentoId",
                table: "Evento",
                column: "OrcamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_EventoItem_CaseId",
                table: "EventoItem",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_EventoItem_EquipamentoId",
                table: "EventoItem",
                column: "EquipamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_EventoItem_EventoId",
                table: "EventoItem",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_EventoItem_InstrumentoId",
                table: "EventoItem",
                column: "InstrumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_EventoItem_SistemaId",
                table: "EventoItem",
                column: "SistemaId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_CidadeId",
                table: "Funcionario",
                column: "CidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_CidadeNascId",
                table: "Funcionario",
                column: "CidadeNascId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_EstadoId",
                table: "Funcionario",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_EstadoNascId",
                table: "Funcionario",
                column: "EstadoNascId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_PaisId",
                table: "Funcionario",
                column: "PaisId");

            migrationBuilder.CreateIndex(
                name: "IX_LocalEvento_CidadeId",
                table: "LocalEvento",
                column: "CidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_LocalEvento_EstadoId",
                table: "LocalEvento",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_LocalEvento_PaisId",
                table: "LocalEvento",
                column: "PaisId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentoFinanceiro_ContaCorrenteId",
                table: "MovimentoFinanceiro",
                column: "ContaCorrenteId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentoFinanceiro_EntidadeId",
                table: "MovimentoFinanceiro",
                column: "EntidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentoFinanceiro_EventoId",
                table: "MovimentoFinanceiro",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentoFinanceiro_FormaPagamentoId",
                table: "MovimentoFinanceiro",
                column: "FormaPagamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentoFinanceiro_PlanoContasId",
                table: "MovimentoFinanceiro",
                column: "PlanoContasId");

            migrationBuilder.CreateIndex(
                name: "IX_Orcamento_CidadeId",
                table: "Orcamento",
                column: "CidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_Orcamento_EntidadeId",
                table: "Orcamento",
                column: "EntidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_Orcamento_EstadoId",
                table: "Orcamento",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Orcamento_LocalEventoId",
                table: "Orcamento",
                column: "LocalEventoId");

            migrationBuilder.CreateIndex(
                name: "IX_Orcamento_ModeloOrcamentoId",
                table: "Orcamento",
                column: "ModeloOrcamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_OrcamentoItem_CaseId",
                table: "OrcamentoItem",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_OrcamentoItem_EquipamentoId",
                table: "OrcamentoItem",
                column: "EquipamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_OrcamentoItem_InstrumentoId",
                table: "OrcamentoItem",
                column: "InstrumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_OrcamentoItem_OrcamentoId",
                table: "OrcamentoItem",
                column: "OrcamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_OrcamentoItem_SistemaId",
                table: "OrcamentoItem",
                column: "SistemaId");

            migrationBuilder.CreateIndex(
                name: "IX_Sistema_FornecedorId",
                table: "Sistema",
                column: "FornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Sistema_InstrumentoId",
                table: "Sistema",
                column: "InstrumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_SistemaCases_CaseId",
                table: "SistemaCases",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_SistemaCases_SistemaId",
                table: "SistemaCases",
                column: "SistemaId");

            migrationBuilder.CreateIndex(
                name: "IX_TabelaPreco_CaseId",
                table: "TabelaPreco",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_TabelaPreco_EquipamentoId",
                table: "TabelaPreco",
                column: "EquipamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_TabelaPreco_SistemaId",
                table: "TabelaPreco",
                column: "SistemaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Almoxarifado");

            migrationBuilder.DropTable(
                name: "CaseEquipamentos");

            migrationBuilder.DropTable(
                name: "Contrato");

            migrationBuilder.DropTable(
                name: "DespesaEvento");

            migrationBuilder.DropTable(
                name: "DespesaViagem");

            migrationBuilder.DropTable(
                name: "Empresa");

            migrationBuilder.DropTable(
                name: "Endereco");

            migrationBuilder.DropTable(
                name: "EventoItem");

            migrationBuilder.DropTable(
                name: "MovimentoFinanceiro");

            migrationBuilder.DropTable(
                name: "OrcamentoItem");

            migrationBuilder.DropTable(
                name: "SistemaCases");

            migrationBuilder.DropTable(
                name: "TabelaPreco");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "PosicaoAlmoxarifado");

            migrationBuilder.DropTable(
                name: "ModeloContrato");

            migrationBuilder.DropTable(
                name: "Montagem");

            migrationBuilder.DropTable(
                name: "ContaCorrente");

            migrationBuilder.DropTable(
                name: "Evento");

            migrationBuilder.DropTable(
                name: "FormaPagamento");

            migrationBuilder.DropTable(
                name: "PlanoContas");

            migrationBuilder.DropTable(
                name: "Equipamento");

            migrationBuilder.DropTable(
                name: "Banco");

            migrationBuilder.DropTable(
                name: "Funcionario");

            migrationBuilder.DropTable(
                name: "Orcamento");

            migrationBuilder.DropTable(
                name: "Case");

            migrationBuilder.DropTable(
                name: "LocalEvento");

            migrationBuilder.DropTable(
                name: "ModeloOrcamento");

            migrationBuilder.DropTable(
                name: "Sistema");

            migrationBuilder.DropTable(
                name: "Entidade");

            migrationBuilder.DropTable(
                name: "Instrumento");

            migrationBuilder.DropTable(
                name: "Cidade");

            migrationBuilder.DropTable(
                name: "Estado");

            migrationBuilder.DropTable(
                name: "Pais");
        }
    }
}
