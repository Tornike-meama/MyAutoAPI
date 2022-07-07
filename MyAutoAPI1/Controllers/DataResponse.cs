
using Microsoft.AspNetCore.Mvc;
using MyAutoAPI1.Models;

namespace MyAutoAPI1.Controllers
{
    public class DataResponse<IData> 
    {
        public static ComonResponse<IData> ReturnResponse(IData data)
        {
            ComonResponse<IData> comonResponse = new ComonResponse<IData>();
            if (data == null)
            {
                comonResponse.isError = true;
                comonResponse.data = default;
                comonResponse.message = "items not found";
                return comonResponse;
            }
            comonResponse.isError = false;
            comonResponse.data = data;
            return comonResponse;
        }
    }
}
