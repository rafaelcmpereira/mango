using AutoMapper;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _response;
        private IMapper _mapper;
        public CouponAPIController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _response = new ResponseDto();
            _mapper = mapper;
        }

        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Coupon> couponList = _db.Coupons.ToList();
                _response.Result = _mapper.Map<IEnumerable<CouponDto>>(couponList); ;
                return _response;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return null;
        }

        [HttpGet]
        [Route("{couponId:int}")]
        public ResponseDto Get(int couponId)
        {
            try
            {
                Coupon coupon = _db.Coupons.First(c => c.CouponId == couponId);

                _response.Result = _mapper.Map<CouponDto>(coupon);
                return _response;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("GetByCode/{couponCode}")]
        public ResponseDto Get(string couponCode)
        {
            try
            {
                Coupon coupon = _db.Coupons.First(c => c.CouponCode.ToLower() == couponCode.ToLower());

                _response.Result = _mapper.Map<CouponDto>(coupon);
                return _response;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpPost]
        public ResponseDto Post([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon coupon = _mapper.Map<Coupon>(couponDto);
                _db.Coupons.Add(coupon);
                _db.SaveChanges();
                _response.Result = _mapper.Map<Coupon>(coupon);
                return _response;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut]
        public ResponseDto Put([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon coupon = _mapper.Map<Coupon>(couponDto);
                _db.Coupons.Update(coupon);
                _db.SaveChanges();
                _response.Result = _mapper.Map<CouponDto>(coupon);
                return _response;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete]

        public ResponseDto Delete(int couponId)
        {
            try
            {
                Coupon coupon = _db.Coupons.First(c => c.CouponId == couponId);
                _db.Coupons.Remove(coupon);
                _db.SaveChanges();
                _response.Result = _mapper.Map<CouponDto>(coupon);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        //[HttpPost]
        //public ResponseDto Post(Coupon coupon)
        //{
        //    try
        //    {
        //        Coupon obj = _db.Coupons.First(c => c.CouponId == coupon.CouponId);

        //        if (obj != null)
        //        {
        //            return null;
        //        }
        //        _db.Coupons.Add(coupon);
        //        _db.SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.Message = ex.Message;
        //    }
        //    return _response;
        //}
    }
}
