using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DyShop.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DyShop.Data.Repositories.Cart
{
    public class CartRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CartRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CartItem> Add(Product product, float quantity, string cartHash)
        {
            var cart = await GetOrCreateCartByHash(cartHash, false);

            var item = GetOrCreateCartItem(product, cart);

            item.Quantity += quantity;

            await _dbContext.SaveChangesAsync();
            
            return item;
        }

        public async Task<Entities.Cart?> ModifyQuantityMany(ICollection<CartItemChangeQuantityRequest> requests, string cartHash)
        {
            var cart = await GetByHash(cartHash);

            if (cart != null)
            {
                var requestDict = requests.ToDictionary(
                    x => x.CartItemId, 
                    x => x.Quantity);
                
                foreach (var item in cart.Items)
                {
                    if (requestDict.ContainsKey(item.Id))
                    {
                        if (requestDict[item.Id] <= 0)
                        {
                            _dbContext.CartItems.Remove(item);
                        }
                        else
                        {
                            item.Quantity = requestDict[item.Id];
                        }
                    }
                }

                await _dbContext.SaveChangesAsync();
            }
            
            return cart;
        }
        
        private CartItem GetOrCreateCartItem(Product product, Entities.Cart cart)
        {
            var cartItem = cart.Items.FirstOrDefault(x => x.Product == product && x.Cart == cart);

            if (cartItem == null)
            {
                cartItem = new CartItem {Product = product, Cart = cart};
                _dbContext.CartItems.Add(cartItem);
            }
            
            return cartItem;
        }

        public async Task<Entities.Cart> GetOrCreateCartByHash(string cartHash, bool commit)
        {
            var cart = _dbContext.Carts.FirstOrDefault(x => x.Hash == cartHash);

            if (cart == null)
            {
                cart = new Entities.Cart {Hash = cartHash};
                _dbContext.Carts.Add(cart);

                if (commit)
                {
                    await _dbContext.SaveChangesAsync();
                }
            }

            return cart;
        }

        public async Task<Entities.Cart?> GetByHash(string cartHash)
        {
            return await _dbContext.Carts.FirstOrDefaultAsync(x => x.Hash == cartHash);
        }

        public async Task RemoveCartItem(string cartHash, int cartItemId)
        {
            var entity = _dbContext.CartItems.FirstOrDefault(x => x.Id == cartItemId && x.Cart.Hash == cartHash);

            if (entity != null)
            {
                _dbContext.CartItems.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task RemoveCart(Entities.Cart cart)
        {
            cart.Items.Clear();
            _dbContext.Carts.Remove(cart);

            await _dbContext.SaveChangesAsync();
        }
    }
}