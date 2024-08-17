using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Plugins.EfCoreSqlServer
{
    public class CartRepository : ICartRepository
    {
        private readonly IMSContext context;

        public CartRepository(IMSContext context)
        {
            this.context = context;
        }

        public async Task AddItemToCartAsync(string userId, int productId, int quantity = 1)
        {
            #region Before the AddToCart Exception
            var cart = await GetCartByUserIdAsync(userId);
            if (cart == null)
            {
                cart = new Cart { UserId = userId };
                context.Carts.Add(cart);
                await context.SaveChangesAsync();
            }

            var cartItem = await context.CartItems.FirstOrDefaultAsync(ci => ci.CartId == cart.CartId && ci.ProductId == productId);
            if (cartItem == null)
            {
                cartItem = new CartItem { CartId = cart.CartId, ProductId = productId, Quantity = quantity };
                context.CartItems.Add(cartItem);
                await context.SaveChangesAsync();
            }
            else
            {
                cartItem.Quantity += quantity;
            }
            await context.SaveChangesAsync();
            #endregion


        }

        public async Task<Cart?> GetCartByUserIdAsync(string userId)
        {
            return await context.Carts
                .Include(cart => cart.Items)
                .ThenInclude(item => item.Product)
                .FirstOrDefaultAsync(cart => cart.UserId == userId);
        }

        public async Task UpdateCartItemAsync(int cartItemId, int quantity)
        {
            var cartItem = await context.CartItems.FindAsync(cartItemId);
            if (cartItem != null)
            {
                cartItem.Quantity = quantity;
                await context.SaveChangesAsync();
            }
        }

        public async Task RemoveCartItemAsync(int cartItemId)
        {
            var cartItem = await context.CartItems.FindAsync(cartItemId);
            if (cartItem != null)
            {
                context.CartItems.Remove(cartItem);
                await context.SaveChangesAsync();
            }
        }

        public async Task<CartItem?> GetCartItemAsync(string userId, int cartItemId)
        {
            return await context.CartItems
                .FirstOrDefaultAsync(ci => ci.Cart.UserId == userId && ci.CartItemId == cartItemId);
        }

        // Branch Item Transfer

        public async Task CreateTransferRequestAsync(string fromUserId, string toUserId, int cartItemId, int quantity)
        {
            var transferRequest = new TransferRequest
            {
                FromUserId = fromUserId,
                ToUserId = toUserId,
                CartItemId = cartItemId,
                Quantity = quantity,
                IsAccepted = false
            };

            context.TransferRequests.Add(transferRequest);
            await context.SaveChangesAsync();
        }

        public async Task<List<TransferRequest>> GetTransferRequestsByUserIdAsync(string userId)
        {
            return await context.TransferRequests
                .Where(tr => tr.ToUserId == userId && !tr.IsAccepted)
                .ToListAsync();
        }

        public async Task AcceptTransferRequestAsync(int transferRequestId)
        {
            var transferRequest = await context.TransferRequests.FindAsync(transferRequestId);
            if (transferRequest != null)
            {
                // Transfer the items as before
                var cartItem = await GetCartItemAsync(transferRequest.FromUserId, transferRequest.CartItemId);
                if (cartItem != null)
                {
                    await UpdateCartItemAsync(transferRequest.CartItemId, cartItem.Quantity - transferRequest.Quantity);
                    await AddItemToCartAsync(transferRequest.ToUserId, cartItem.ProductId, transferRequest.Quantity);
                }

                transferRequest.IsAccepted = true;
                await context.SaveChangesAsync();
            }
        }

        public async Task CancelTransferRequestAsync(int transferRequestId)
        {
            var transferRequest = await context.TransferRequests.FindAsync(transferRequestId);
            if (transferRequest != null)
            {
                context.TransferRequests.Remove(transferRequest);
                await context.SaveChangesAsync();
            }
        }


    }
}
