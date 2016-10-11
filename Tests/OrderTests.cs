using System;
using Xunit;
using Bangazon.Orders;
using System.Collections.Generic;

namespace Bangazon.Tests
{
    public class OrderTests
    {
        [Fact]
        public void TestTheTestingFramework()
        {
            Assert.True(true);  
        }

        [Fact]
        public void OrdersCanExist ()
        {
            Order ord = new Order ();
            Assert.NotNull(ord);
        }

        [Fact]
        public void NewOrdersHaveAGuidofTypeGuid()
        {
            Order ord = new Order ();
            Assert.NotNull(ord.orderNumber);
            Assert.IsType<Guid>(ord.orderNumber);
        }

        [Fact]

        public void NewOrderShouldHaveAnEmptyProductListofStrings()
        {
            Order ord = new Order ();
            Assert.NotNull(ord.products);
            Assert.IsType<List<string>>(ord.products);
            Assert.Empty(ord.products);
        }

        [Theory]
        [InlineData("Banana")]
        [InlineDataAttribute("76456456")]
        [InlineDataAttribute("Banana with space")]
        [InlineDataAttribute("Banana, comma?")]

        public void OrdersCanHaveProductsAddedToThem(string product)
        {
            Order ord = new Order();
            ord.addProduct(product);
            Assert.Equal(1, ord.products.Count);
            Assert.Contains<string>(product, ord.products);
        }
        // this is how the theory above would look like as a FACT but would have to do 1 fact at a time for all strings in theory. 
        [Fact]
        public void OrdersCanHaveProductAddedToThem()
        {
            Order ord = new Order();
            ord.addProduct("someProduct");
            Assert.Equal(1, ord.products.Count);
            Assert.Contains<string>("someProduct", ord.products);
        }

        [Theory]
        [InlineData("Banana")]
        [InlineDataAttribute("Banana,another banana")]
        [InlineDataAttribute("Banana,second banana,thirdBanana")]
        [InlineDataAttribute("Banana 1,banana 2,banana 3,banana 4")]


        public void OrdersCanHaveMultipleProductsAddedToThem(string productString)
        {
            string [] products = productString.Split(new char[] {','});
            // can actually split on multiple characters by seperating by ,
            Order ord = new Order();
            foreach (string product in products)
            {
                ord.addProduct(product);
            }
            Assert.Equal(products.Length, ord.products.Count);
            foreach (string product in products)
            {
                Assert.Contains<string>(product, ord.products);
            }
        }

        [Theory]
        [InlineData("product")]
        [InlineDataAttribute("product,another product")]
        [InlineDataAttribute("product,second product,third product")]
        [InlineDataAttribute("product 1,product 2,product 3,product 4")]


        public void OrdersCanListProductsForTerminalDisplay(string productString)
        {
            string [] products = productString.Split(new char[] {','});
            // can actually split on multiple characters by seperating by ,
            Order ord = new Order();
            foreach (string product in products)
            {
                ord.addProduct(product);
            }
            foreach (string product in products)
            {
                Assert.Contains($"\nYou ordered {product}", ord.listProducts());
            }
        }
        // here we are using the TDD principle to test the removal of a product which is code that hasnt been removed yet
        [Fact]
        public void OrdersCanHaveAProductRemovedFromThem ()
        {
            Order ord = new Order ();
            ord.addProduct("product");
            ord.addProduct("banana bread");
            ord.addProduct("banana");
            ord.addProduct("apple");
            
            ord.removeProduct("banana");
            
            Assert.Equal(3, ord.products.Count);
            Assert.DoesNotContain<string>("banana", ord.products);
            
        }

        [Fact]
        public void OrdersCanNotRemoveAProductThatDoesNotExistFromThem ()
        {
            Order ord = new Order ();
            ord.addProduct("product");
            ord.addProduct("banana bread");
            ord.addProduct("banana");
            ord.addProduct("apple");
            
            ord.removeProduct("pineapple");
            
            Assert.Equal(4, ord.products.Count);
        }

        [Theory]
        [InlineDataAttribute("banana")]
        [InlineDataAttribute("pineapple")]

        public void RemoveMethodReturnsBooleanIndicatingIfProductWasRemoved(string product)
        {
            Order ord = new Order ();
            ord.addProduct("product");
            ord.addProduct("banana bread");
            ord.addProduct("banana");
            ord.addProduct("apple");

            bool removed = ord.removeProduct(product);

            if (product == "banana")
            {
                Assert.True(removed);
            }
            if (product == "pineapple")
            {
                Assert.False(removed);
            }
        }

        [Fact]

        public void AllProductsFromAnOrderCanBeDeleted()
        {
            Order ord = new Order ();
            ord.addProduct("product");
            ord.addProduct("banana bread");
            ord.addProduct("banana");
            ord.addProduct("apple");

            ord.removeProduct();

            Assert.Empty(ord.products);
        }
    }
}