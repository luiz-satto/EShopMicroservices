using Discount.GRPC.Data;
using Discount.GRPC.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.GRPC.Services
{
    public class DiscountService(
        DiscountContext dbContext,
        ILogger<DiscountService> logger
    ) : DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContext.Coupons
                .FirstOrDefaultAsync(x => x.ProductName == request.ProductName)
                ?? new Coupon
                {
                    ProductName = "No Discount",
                    Amount = 0,
                    Description = "No Discount",
                };

            logger.LogInformation("Discount is retrieved for ProductName : {productName}, Amount : {amount}", coupon.ProductName, coupon.Amount);

            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            if (request.Coupon.Id != 0)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Coupon Id."));
            }

            return await UpsertDiscount(request.Coupon);
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            if (request.Coupon.Id == 0)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Coupon Id."));
            }

            return await UpsertDiscount(request.Coupon);
        }

        private async Task<CouponModel> UpsertDiscount(CouponModel model)
        {
            var coupon = model.Adapt<Coupon>()
                ?? throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object."));

            if (coupon.Id == 0)
            {
                dbContext.Coupons.Add(coupon);
            }
            else
            {
                dbContext.Coupons.Update(coupon);
            }

            await dbContext.SaveChangesAsync();

            var logCreatedUpdated = coupon.Id == 0 ? "created" : "updated";
            logger.LogInformation($"Discount is successfully {logCreatedUpdated}. ProductName : {coupon.ProductName}");

            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContext.Coupons
                .FirstOrDefaultAsync(x => x.ProductName == request.ProductName)
                ?? throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName={request.ProductName}"));

            dbContext.Coupons.Remove(coupon);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Discount is successfully deleted. ProductName : {ProductName}", coupon.ProductName);

            return new DeleteDiscountResponse() { Success = true };
        }
    }
}
