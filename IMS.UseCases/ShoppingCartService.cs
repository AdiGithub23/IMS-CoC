﻿using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.UseCases
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly ICartRepository cartRepository;
        private readonly ILogger<ShoppingCartService> logger;

        public ShoppingCartService(ICartRepository cartRepository, ILogger<ShoppingCartService> logger)
        {
            this.cartRepository = cartRepository;
            this.logger = logger;
        }

        public async Task AddItemToCartAsync(string userId, int productId, int quantity)
        {
            if (string.IsNullOrEmpty(userId))
            {
                logger.LogWarning("UserId Cannot be Null or Empty");
                return;
            }
            else
            {
                await cartRepository.AddItemToCartAsync(userId, productId, quantity);
            }

        }

        public async Task<Cart?> GetCartByUserIdAsync(string userId)
        {
            return await cartRepository.GetCartByUserIdAsync(userId);
        }

        public async Task UpdateCartItemAsync(int cartItemId, int quantity)
        {
            await cartRepository.UpdateCartItemAsync(cartItemId, quantity);
        }

        public async Task RemoveCartItemAsync(int cartItemId)
        {
            await cartRepository.RemoveCartItemAsync(cartItemId);
        }

        public async Task TransferItemsAsync(string fromUserId, string toUserId, int cartItemId, int quantity)
        {
            try
            {
                // Get the cart item from the source user's cart
                var cartItem = await cartRepository.GetCartItemAsync(fromUserId, cartItemId);

                if (cartItem != null)
                {
                    // Remove items from the source cart
                    await cartRepository.UpdateCartItemAsync(cartItemId, cartItem.Quantity - quantity);

                    // Add items to the destination user's cart
                    await cartRepository.AddItemToCartAsync(toUserId, cartItem.ProductId, quantity);
                }
                else
                {
                    throw new Exception("Cart item not found.");
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Error transferring items: {ex.Message}");
                throw;
            }
        }

    }
}
