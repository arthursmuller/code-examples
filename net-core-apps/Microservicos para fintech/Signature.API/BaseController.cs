using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Signature.Domain.Dtos;
using Signature.Domain.Responses;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Signature.API
{
    public class BaseController : Controller
    {
        public BaseController() { }

        protected IActionResult RespondOk<T>(T objRetorno, string message = null)
        {
            return Ok(new DefaultResponse<T>(objRetorno) { Message = message });
        }

        protected IActionResult RespondOk(string value)
        {
            return Ok(new DefaultResponse<string>(value));
        }

        protected IActionResult RespondOk<T>(T objRetorno)
        {
            return Ok(objRetorno);
        }

        protected IActionResult RespondCreated<T>(T objRetorno, string message = null)
        {
            return CreatedAtAction(nameof (objRetorno), new DefaultResponse<T>() { Message = message });
        }

        protected IActionResult RespondOkBadReq(string message)
        {
            return BadRequest(new DefaultResponse<object>() { Message = message });
        }

        protected FileContentResult file(DownloadDto downloadDto)
        {
            var result = new FileContentResult(downloadDto.Blob, downloadDto.MimeType)
            {
                FileDownloadName = $"{downloadDto.FileName}.{downloadDto.Extension}"
            };

            return result;
        }

        protected string base64(IFormFile formFile)
        {
            if(formFile is null) 
                return default; 

            byte[] fileData;
            using (var binaryReader = new BinaryReader(formFile.OpenReadStream()))
            {
                fileData = binaryReader.ReadBytes((int)formFile.Length);
            }
            return Convert.ToBase64String(fileData);
        }
    }
}


