using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MindSphereSdk.AspNetCore;
using MindSphereSdk.Asset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("api")]
    public class MainController : ControllerBase
    {
        IMindSphereSdkService _mindSphereSdkService;

        public MainController(IMindSphereSdkService mindSphereSdkService)
        {
            _mindSphereSdkService = mindSphereSdkService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssetResponse>>> Get()
        {
            var assetClient = _mindSphereSdkService.GetAssetClient();
            return StatusCode(200, await assetClient.GetAssetsAsync());
        }
    }
}
