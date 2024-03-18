using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplicacao.Interfaces;
using Aplicacao.Model.Documento;
using Aplicacao.Servico;
using B.Mensagens.Interfaces;
using Dominio;
using Dominio.Enum;
using Infraestrutura;
using Infraestrutura.Providers;
using Microsoft.EntityFrameworkCore;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;

namespace Aplicacao
{
    public sealed class DocumentoServico : ServicoBase, IDocumentoServico
    {
        private readonly IProviderAzure _providerAzure;

        public DocumentoServico(
            PlataformaClienteContexto contexto,
            IUsuarioLogin usuarioLogin,
            IBemMensagens mensagens,
            IProviderAzure providerAzure) : base(mensagens, usuarioLogin, contexto)
            => _providerAzure = providerAzure;

        public async Task<DocumentoModel> GerarPdfTermoSeguro(
            UsuarioDominio usuario,
            SeguroPropostaDominio seguroProposta,
            SeguroPropostaIcatuDominio seguroPropostaIcatu,
            SeguroProdutoIcatuDominio seguroProdutoIcatu,
            SeguroClienteIcatuDominio seguroClienteIcatu,
            SeguroProfissaoIcatuDominio seguroProfissaoIcatu,
            SeguroCoberturaDominio seguroCoberturaCodigo2,
            SeguroCoberturaDominio seguroCoberturaCodigo24,
            SeguroCoberturaDominio seguroCoberturaCodigo26,
            SeguroClienteTelefoneDominio telefoneCliente,
            List<SeguroBeneficiarioDominio> seguroBeneficiarios,
            SeguroCobrancaPropostaCartaoIcatuDominio seguroCobrancaPropostaCartaoIcatu)
        {
            var genero = await _contexto.Generos.FirstOrDefaultAsync(e => e.ID.Equals(seguroClienteIcatu.IdGenero));
            var idOrgaoEmissor = usuario.Cliente.DocumentosIdentificacao.FirstOrDefault(e => e.IdTipoDocumento == TipoDocumento.CarteiraNacionalDeHabilitacao)?.IdOrgaoEmissor;
            var orgaoEmissorIdentificacao = await _contexto.OrgaosEmissoresIdentificacao.FirstOrDefaultAsync(e => e.ID.Equals(idOrgaoEmissor));

            var documento = criarDocumento(new List<IEnumerable<KeyValuePair<string, PosicaoTextoArquivo>>>
            {
                popularDocumentoPagina1(usuario, seguroProposta, seguroPropostaIcatu, seguroProdutoIcatu, seguroClienteIcatu, seguroProfissaoIcatu, seguroCoberturaCodigo2, seguroCoberturaCodigo24, seguroCoberturaCodigo26, telefoneCliente, seguroCobrancaPropostaCartaoIcatu, orgaoEmissorIdentificacao, genero)
                .Concat(popularBeneficiarios(seguroBeneficiarios)),
                popularDocumentoPagina3(usuario.Cliente, seguroProposta)
            });

            var urlArquivo = await salvarArquivoAzure(documento, ".pdf");
            seguroProposta.SetUrlPdfContrato(urlArquivo);
            await SaveChangesAsync();

            return new DocumentoModel
            {
                /*Arquivo = Convert.ToBase64String(documento),*/
                UrlArquivo = urlArquivo
            };
        }

        private byte[] criarDocumento<T>(IEnumerable<T> paginas) where T : IEnumerable<KeyValuePair<string, PosicaoTextoArquivo>>
        {
            var paginasList = paginas.ToList();
            var basePath = Path.Combine(Environment.CurrentDirectory, "Documentos");

            var pagina1 = criarPaginaDocumento(Path.Combine(basePath, "proposta-seguro_pg01.jpg"), paginasList[0]);
            var pagina2 = criarPaginaDocumento(Path.Combine(basePath, "proposta-seguro_pg02.jpg"), null);
            var pagina3 = criarPaginaDocumento(Path.Combine(basePath, "proposta-seguro_pg03.jpg"), paginasList[1]);

            return unificarDocumentos(pagina1, new byte[][] { pagina2, pagina3 });
        }

        private byte[] criarPaginaDocumento(string file, IEnumerable<KeyValuePair<string, PosicaoTextoArquivo>> valores)
        {
            Bitmap newBitmap;
            using (var bitmap = (Bitmap)Image.FromFile(file))
            {
                if (valores == null)
                {
                    Bitmap tempBitmap = new Bitmap(bitmap.Width, bitmap.Height);
                    using (Graphics graphics = Graphics.FromImage(tempBitmap))
                    {
                        graphics.DrawImage(bitmap, 0, 0);
                    }
                }
                else
                {
                    using (Graphics graphics = Graphics.FromImage(bitmap))
                    {
                        foreach (var valor in valores)
                        {
                            using (Font arialFont = new Font("Arial", 7))
                            {
                                PointF localizacao = new PointF(valor.Value.PositionX, valor.Value.PositionY);
                                graphics.DrawString(valor.Key, arialFont, Brushes.Black, localizacao);
                            }
                        }
                    }
                }

                newBitmap = new Bitmap(bitmap);

                using (var ms = new MemoryStream())
                {
                    newBitmap.Save(ms, ImageFormat.Png);
                    PdfDocument documento = new PdfDocument();
                    PdfPage pagina = documento.AddPage();
                    ms.Position = 0;
                    XImage imagem = XImage.FromStream(() => ms);
                    XGraphics xGraphics = XGraphics.FromPdfPage(pagina);

                    xGraphics.DrawImage(imagem, 0, 0, pagina.Width, pagina.Height);
                    using (var msDocumento = new MemoryStream())
                    {
                        documento.Save(msDocumento);
                        return msDocumento.ToArray();
                    }
                }
            }
        }

        private static List<KeyValuePair<string, PosicaoTextoArquivo>> popularDocumentoPagina1(
            UsuarioDominio usuario,
            SeguroPropostaDominio seguroProposta,
            SeguroPropostaIcatuDominio seguroPropostaIcatu,
            SeguroProdutoIcatuDominio seguroProdutoIcatu,
            SeguroClienteIcatuDominio seguroClienteIcatu,
            SeguroProfissaoIcatuDominio seguroProfissaoIcatu,
            SeguroCoberturaDominio seguroCoberturaCodigo2,
            SeguroCoberturaDominio seguroCoberturaCodigo24,
            SeguroCoberturaDominio seguroCoberturaCodigo26,
            SeguroClienteTelefoneDominio telefoneCliente,
            SeguroCobrancaPropostaCartaoIcatuDominio seguroCobrancaPropostaCartaoIcatu,
            OrgaoEmissorIdentificacaoDominio orgaoEmissorIdentificacao,
            GeneroDominio genero)
            => new List<KeyValuePair<string, PosicaoTextoArquivo>>
            {
                new KeyValuePair<string, PosicaoTextoArquivo>("X", new PosicaoTextoArquivo(133, 125)),
                new KeyValuePair<string, PosicaoTextoArquivo>(seguroProposta.NumeroPropostaIcatu.ToString(), new PosicaoTextoArquivo(550, 125)),
                new KeyValuePair<string, PosicaoTextoArquivo>("82017690", new PosicaoTextoArquivo(60, 200)),
                new KeyValuePair<string, PosicaoTextoArquivo>("", new PosicaoTextoArquivo(179, 200)),
                new KeyValuePair<string, PosicaoTextoArquivo>("0", new PosicaoTextoArquivo(640, 200)),
                new KeyValuePair<string, PosicaoTextoArquivo>(seguroProdutoIcatu.Modulo.ToString(), new PosicaoTextoArquivo(800, 200)),
                new KeyValuePair<string, PosicaoTextoArquivo>(seguroProdutoIcatu.DataInicioVigencia.ToString("dd/MM/yyyy"), new PosicaoTextoArquivo(1000, 200)),
                new KeyValuePair<string, PosicaoTextoArquivo>(seguroClienteIcatu.Nome.ToString(), new PosicaoTextoArquivo(60, 280)),
                new KeyValuePair<string, PosicaoTextoArquivo>("X", getPosicaoTextoGenero(genero.Sigla)),
                new KeyValuePair<string, PosicaoTextoArquivo>(seguroClienteIcatu.DataNascimento?.ToString("dd/MM/yyyy"), new PosicaoTextoArquivo(850, 280)),
                new KeyValuePair<string, PosicaoTextoArquivo>(usuario.CPF, new PosicaoTextoArquivo(60, 335)),
                new KeyValuePair<string, PosicaoTextoArquivo>(usuario.Cliente.DocumentosIdentificacao?.FirstOrDefault()?.Numero, new PosicaoTextoArquivo(300, 335)),
                new KeyValuePair<string, PosicaoTextoArquivo>(orgaoEmissorIdentificacao?.Descricao, new PosicaoTextoArquivo(584, 335)),
                new KeyValuePair<string, PosicaoTextoArquivo>(usuario.Cliente.DocumentosIdentificacao?.FirstOrDefault()?.DataEmissao.ToString(), new PosicaoTextoArquivo(760, 335)),
                new KeyValuePair<string, PosicaoTextoArquivo>(seguroProfissaoIcatu.Descricao, new PosicaoTextoArquivo(938, 335)),
                new KeyValuePair<string, PosicaoTextoArquivo>("X", getPosicaoTextoResidentePais(seguroClienteIcatu.ResidentePais)),
                new KeyValuePair<string, PosicaoTextoArquivo>(getLogradouro(seguroClienteIcatu), new PosicaoTextoArquivo(260, 390)),
                new KeyValuePair<string, PosicaoTextoArquivo>(getNumero(seguroClienteIcatu), new PosicaoTextoArquivo(920, 390)),
                new KeyValuePair<string, PosicaoTextoArquivo>(getComplemento(seguroClienteIcatu), new PosicaoTextoArquivo(1100, 390)),
                new KeyValuePair<string, PosicaoTextoArquivo>(getBairro(seguroClienteIcatu), new PosicaoTextoArquivo(60, 440)),
                new KeyValuePair<string, PosicaoTextoArquivo>(getMunicipio(seguroClienteIcatu), new PosicaoTextoArquivo(310, 440)),
                new KeyValuePair<string, PosicaoTextoArquivo>(getCep(seguroClienteIcatu), new PosicaoTextoArquivo(630, 440)),
                new KeyValuePair<string, PosicaoTextoArquivo>(getUf(seguroClienteIcatu), new PosicaoTextoArquivo(803, 440)),
                new KeyValuePair<string, PosicaoTextoArquivo>(telefoneCliente?.DDD, new PosicaoTextoArquivo(925, 440)),
                new KeyValuePair<string, PosicaoTextoArquivo>(telefoneCliente?.Fone, new PosicaoTextoArquivo(990, 440)),
                new KeyValuePair<string, PosicaoTextoArquivo>(seguroClienteIcatu.Email, new PosicaoTextoArquivo(60, 490)),
                new KeyValuePair<string, PosicaoTextoArquivo>("X", getPosicaoTextoRelacionamentoEletronico(seguroClienteIcatu.RelacionamentoEletronico)),
                new KeyValuePair<string, PosicaoTextoArquivo>("X", getPosicaoTextoPPE(seguroClienteIcatu.PPE)),
                new KeyValuePair<string, PosicaoTextoArquivo>(seguroClienteIcatu.RendaMensal.ToString(), new PosicaoTextoArquivo(60, 540)),
                new KeyValuePair<string, PosicaoTextoArquivo>("X", getPosicaoTextoAposentado(seguroClienteIcatu.Aposentado)),
                new KeyValuePair<string, PosicaoTextoArquivo>(seguroPropostaIcatu.ValorPremioTotal.ToString(), new PosicaoTextoArquivo(980, 700)),
                new KeyValuePair<string, PosicaoTextoArquivo>(seguroCobrancaPropostaCartaoIcatu.Titular, new PosicaoTextoArquivo(60, 865)),
                new KeyValuePair<string, PosicaoTextoArquivo>(seguroCobrancaPropostaCartaoIcatu.CpfTitular, new PosicaoTextoArquivo(710, 865)),
                new KeyValuePair<string, PosicaoTextoArquivo>("X", getPosicaoTextoValorCapital2(seguroCoberturaCodigo2?.ValorCapital)),
                new KeyValuePair<string, PosicaoTextoArquivo>(seguroCoberturaCodigo2?.ValorCapital.ToString(), new PosicaoTextoArquivo(930, 1037)),
                new KeyValuePair<string, PosicaoTextoArquivo>(seguroCoberturaCodigo24?.ValorCapital.ToString(), new PosicaoTextoArquivo(930, 1061)),
                new KeyValuePair<string, PosicaoTextoArquivo>(seguroCoberturaCodigo26?.ValorCapital.ToString(), new PosicaoTextoArquivo(930, 1087)),
                new KeyValuePair<string, PosicaoTextoArquivo>(seguroProposta.DataProtocolo.ToString("dd/MM/yyyy"), new PosicaoTextoArquivo(60, 1980)),
                new KeyValuePair<string, PosicaoTextoArquivo>(usuario.CPF, new PosicaoTextoArquivo(450, 1980))
            };

        private static List<KeyValuePair<string, PosicaoTextoArquivo>> popularBeneficiarios(List<SeguroBeneficiarioDominio> seguroBeneficiarios)
        {
            var list = new List<KeyValuePair<string, PosicaoTextoArquivo>>();
            var i = 0;

            if (!(seguroBeneficiarios is null))
            {
                foreach (var item in seguroBeneficiarios)
                {
                    var seguroBeneficiarioNome = new KeyValuePair<string, PosicaoTextoArquivo>(item.Nome, new PosicaoTextoArquivo(60, i == 0 ? 1250 : 1280));
                    var seguroBeneficiarioParentesco = new KeyValuePair<string, PosicaoTextoArquivo>(item.SeguroParentesco.Descricao, new PosicaoTextoArquivo(870, i == 0 ? 1250 : 1280));
                    var seguroBeneficarioValorPercentual = new KeyValuePair<string, PosicaoTextoArquivo>(item.ValorPercentual.ToString(), new PosicaoTextoArquivo(1050, i == 0 ? 1250 : 1280));

                    list.Add(seguroBeneficiarioNome);
                    list.Add(seguroBeneficiarioParentesco);
                    list.Add(seguroBeneficarioValorPercentual);

                    i++;
                }

                return list;
            }

            return default;
        }

        private static List<KeyValuePair<string, PosicaoTextoArquivo>> popularDocumentoPagina3(ClienteDominio cliente, SeguroPropostaDominio seguroProposta)
            => new List<KeyValuePair<string, PosicaoTextoArquivo>>
            {
                new KeyValuePair<string, PosicaoTextoArquivo>(retornaMD5(seguroProposta.ID.ToString()), new PosicaoTextoArquivo(265, 417)),
                new KeyValuePair<string, PosicaoTextoArquivo>(retornaMD5(cliente.ID.ToString()), new PosicaoTextoArquivo(265, 500)),
                new KeyValuePair<string, PosicaoTextoArquivo>("IP Origem: " + seguroProposta.IPOrigem, new PosicaoTextoArquivo(265, 530)),
                new KeyValuePair<string, PosicaoTextoArquivo>("Longitude: " + seguroProposta.Longitude.ToString(), new PosicaoTextoArquivo(265, 560)),
                new KeyValuePair<string, PosicaoTextoArquivo>("Latitude: " + seguroProposta.Latitude.ToString(), new PosicaoTextoArquivo(265, 590)),
                new KeyValuePair<string, PosicaoTextoArquivo>(seguroProposta.DataAtualizacao.ToString("dd/MM/yyyy HH:mm:ss"), new PosicaoTextoArquivo(1000, 1900))
            };
     
        private byte[] unificarDocumentos(byte[] documentoPrincipal, params byte[][] documentosAdicionais)
        {
            using (var msDocumentoPrincipal = new MemoryStream(documentoPrincipal))
            {
                PdfDocument documento = PdfReader.Open(msDocumentoPrincipal);

                foreach (var doc in documentosAdicionais)
                {
                    using (var streamDocumentosAdicionais = new MemoryStream(doc))
                    {
                        PdfDocument documentoAdicional = PdfReader.Open(streamDocumentosAdicionais, PdfDocumentOpenMode.Import);
                        documento.AddPage(documentoAdicional.Pages[0]);
                    }
                }

                using (var ms = new MemoryStream())
                {
                    documento.Save(ms);
                    return ms.ToArray();
                }
            }
        }

        private static PosicaoTextoArquivo getPosicaoTextoRelacionamentoEletronico(bool relacionamentoEletronico)
            => relacionamentoEletronico ? new PosicaoTextoArquivo(768, 492) : new PosicaoTextoArquivo(847, 492);

        private static PosicaoTextoArquivo getPosicaoTextoPPE(char ppe)
            => ppe == '0' ? new PosicaoTextoArquivo(976, 490) : new PosicaoTextoArquivo(976, 492);

        private static PosicaoTextoArquivo getPosicaoTextoValorCapital2(decimal? valor)
            => valor == 10000 ? new PosicaoTextoArquivo(60, 972) : new PosicaoTextoArquivo(255, 972);

        private static PosicaoTextoArquivo getPosicaoTextoAposentado(bool aposentado)
          => aposentado ? new PosicaoTextoArquivo(207, 544) : new PosicaoTextoArquivo(265, 544);

        private static PosicaoTextoArquivo getPosicaoTextoResidentePais(bool residentePais)
            => residentePais ? new PosicaoTextoArquivo(60, 388) : new PosicaoTextoArquivo(130, 388);

        private static PosicaoTextoArquivo getPosicaoTextoGenero(string sigla)
            => sigla == "F" ? new PosicaoTextoArquivo(738, 278) : new PosicaoTextoArquivo(649, 278);

        private static string getLogradouro(SeguroClienteIcatuDominio seguroClienteIcatu)
          => seguroClienteIcatu.EnderecoPrincipal is null ? seguroClienteIcatu.EnderecoCobranca.Logradouro : seguroClienteIcatu.EnderecoPrincipal.Logradouro;

        private static string getNumero(SeguroClienteIcatuDominio seguroClienteIcatu)
          => seguroClienteIcatu.EnderecoPrincipal is null ? seguroClienteIcatu.EnderecoCobranca.Numero.ToString() : seguroClienteIcatu.EnderecoPrincipal.Numero.ToString();

        private static string getComplemento(SeguroClienteIcatuDominio seguroClienteIcatu)
          => seguroClienteIcatu.EnderecoPrincipal is null ? seguroClienteIcatu.EnderecoCobranca.Complemento : seguroClienteIcatu.EnderecoPrincipal.Complemento;

        private static string getBairro(SeguroClienteIcatuDominio seguroClienteIcatu)
          => seguroClienteIcatu.EnderecoPrincipal is null ? seguroClienteIcatu.EnderecoCobranca.Bairro : seguroClienteIcatu.EnderecoPrincipal.Bairro;

        private static string getMunicipio(SeguroClienteIcatuDominio seguroClienteIcatu)
          => seguroClienteIcatu.EnderecoPrincipal is null ? seguroClienteIcatu.EnderecoCobranca.Municipio.Descricao : seguroClienteIcatu.EnderecoPrincipal.Municipio.Descricao;

        private static string getCep(SeguroClienteIcatuDominio seguroClienteIcatu)
          => seguroClienteIcatu.EnderecoPrincipal is null ? seguroClienteIcatu.EnderecoCobranca.Cep : seguroClienteIcatu.EnderecoPrincipal.Cep;

        private static string getUf(SeguroClienteIcatuDominio seguroClienteIcatu)
          => seguroClienteIcatu.EnderecoPrincipal is null ? seguroClienteIcatu.EnderecoCobranca.Municipio.UF.Nome : seguroClienteIcatu.EnderecoPrincipal.Municipio.UF.Nome;

        private static string retornaMD5(string texto)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(texto);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }

                return sb.ToString();
            }
        }

        private async Task<string> salvarArquivoAzure(byte[] documento, string extensao)
            => await _providerAzure.SalvarArquivo(documento, extensao);
    }
}