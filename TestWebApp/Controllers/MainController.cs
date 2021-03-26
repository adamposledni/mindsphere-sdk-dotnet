using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MindSphereLibrary.AspNetCore;
using MindSphereLibrary.Asset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebApp.Controllers
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
            return StatusCode(200, await assetClient.ListAssetsAsync());
        }
    }
}
