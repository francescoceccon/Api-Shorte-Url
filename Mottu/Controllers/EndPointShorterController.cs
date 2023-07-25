using Application.Services.Interfaces;
using Domain;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Mottu.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class EndPointShorterController : ControllerBase
    {
        /// <summary>
        /// Endpoint para receber o array de jsons com as urls
        /// </summary>
        /// <param name="_urlService">Service de Processamento injetado</param>
        /// <param name="mottuUrls">Array modelo</param>
        /// <returns>200</returns>
        [HttpPost]
        public async Task<IActionResult> ConsumeMottuJsonReceivingAsync([FromServices] IURLService _urlService, [FromBody] List<MottuUrl> mottuUrls)
        {
            await _urlService.ProcessMottuUrlsAsync(mottuUrls);
            return Ok();
        }

        /// <summary>
        /// Endpoint para consumir internamente o json para a app
        /// </summary>
        /// <param name="_urlService">Service de Processamento injetado</param>
        /// <param name="mottuUrls">Array modelo</param>
        /// <returns>200</returns>
        [HttpPost]
        public async Task<IActionResult> ConsumeMottuJsonInternalAsync([FromServices] IURLService _urlService)
        {
            await _urlService.ProcessMottuUrlsAsync(null);
            return Ok();
        }

        /// <summary>
        /// Endpoint para persistir uma longUrl de sua preferencia
        /// Esse endpoint vai processar a longUrl para uma shorUrl
        /// </summary>
        /// <param name="_urlService">Service de Processamento injetado</param>
        /// <param name="longUrl">Url Longa</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UrlShorterAsync([FromServices] IURLService _urlService,[FromForm] string longUrl)
        {
            var mottuUrl = await _urlService.ProcessShortUrlAsync(longUrl);
            return Created("GetUrlAsync", mottuUrl.Id);
        }

        /// <summary>
        /// Endpoint para redirecionamento de rota e incremento 'hit' da mesma
        /// </summary>
        /// <param name="_urlService">Service de Processamento injetado</param>
        /// <param name="shortUrl">Url encurtada</param>
        /// <returns>302 ou 400</returns>
        [HttpGet]
        public async Task<IActionResult> RedirectUrl([FromServices] IURLService _urlService,[FromQuery] string shortUrl)
        {
            var urlRedirect = await _urlService.ReadUrlAsync(shortUrl);
            if (urlRedirect != null)
            {
                await _urlService.UpdateAsync(urlRedirect);

               return Redirect(urlRedirect.Url);
            }
            return BadRequest();
        }

        /// <summary>
        /// Endpoint para ler uma entidade do PostGres recebendo uma shortUrl
        /// </summary>
        /// <param name="_urlService">Service de Processamento injetado</param>
        /// <param name="shortUrl">Url Encurtada</param>
        /// <returns>200</returns>
        [HttpGet]
        public async Task<IActionResult> GetMottuUrlAsync([FromServices] IURLService _urlService, [FromQuery] string shortUrl)
        {
            var urlRedirect = await _urlService.ReadUrlAsync(shortUrl);
            return Ok(urlRedirect);
        }

        /// <summary>
        /// Endpoint para ler uma entidade do PostGres recebendo o Id da mesma
        /// </summary>
        /// <param name="_urlService">Service de Processamento injetado</param>
        /// <param name="id">Id da entidade</param>
        /// <returns>200</returns>
        [HttpGet("id")]
        public async Task<IActionResult> GetUrlAsync([FromServices] IURLService _urlService,[FromQuery] string id)
        {
            var mottuUrl = await _urlService.GetUrlAsync(id);
            return Ok(mottuUrl);
        }

        /// <summary>
        /// Endpoint para delete da entidade do PostGres
        /// </summary>
        /// <param name="_urlService">Service de Processamento injetado</param>
        /// <param name="id">Id da entidade</param>
        /// <returns>204</returns>
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteUrlAsync([FromServices] IURLService _urlService, [FromQuery] string id)
        {
            await _urlService.DeleteUrlAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Endpoint para update da entidade
        /// </summary>
        /// <param name="_uRLService">Service de Processamento injetado</param>
        /// <param name="mottuUrl">Entidade a ser Atualizada</param>
        /// <returns>204</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateUrlAsync([FromServices] IURLService _urlService, [FromForm] MottuUrlDTO mottuUrl)
        {
            await _urlService.UpdateAsync(mottuUrl);
            return NoContent();
        }
    }
}
