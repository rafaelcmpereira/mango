using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _db;

        public CouponAPIController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public object Get()
        {
            try
            {
                IEnumerable<Coupon> objList = _db.Coupons.ToList();
                return objList;
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        [HttpGet]
        [Route("{id:int}")]
        public object Get(int id)
        {
            try
            {
                IEnumerable<Coupon> objList = _db.Coupons.Where(c => c.CouponId == id).ToList();
                return objList;
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        [HttpPost]
        public object Post(Coupon coupon)
        {
            try
            {
                IEnumerable<Coupon> objList = _db.Coupons.Where(c => c.CouponId == coupon.CouponId).ToList();

                if (objList.Count() >= 1)
                {
                    return null;
                }
                _db.Coupons.Add(coupon);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
            return null;
        }
    }
}
