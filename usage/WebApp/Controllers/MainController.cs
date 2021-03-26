using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MindSphereSdk.AspNetCore;
using MindSphereSdk.AssetManagement;
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

        [HttpGet("list-assets")]
        public async Task<ActionResult<IEnumerable<AssetResource>>> ListAssets()
        {
            var assetClient = _mindSphereSdkService.GetAssetClient();
            var request = new ListAssetsRequest()
            {
                Size = 1
            };
            return StatusCode(200, await assetClient.ListAssetsAsync(request));
        }

        [HttpGet("add-asset")]
        public async Task<ActionResult<AssetResource>> AddAsset()
        {
            var assetClient = _mindSphereSdkService.GetAssetClient();
            var request = new AddAssetRequest()
            {
                Body = new AssetResource()
                {
                    Name = "MyNewAsset",
                    TypeId = "iiotdgli.mobilephone",
                    ParentId = "ec206f76b04a49a4938c1573b35b6688",
                }
            };
            return StatusCode(200, await assetClient.AddAssetsAsync(request));
        }
    }
}
