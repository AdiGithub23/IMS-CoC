﻿using IMS.CoreBusiness;
using IMS.CoreBusiness.DTO;
using IMS.UseCases.PluginInterfaces;
using IMS.UseCases.Products.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IMS.UseCases.Products
{
    public class ViewProductsByNameUseCase : IViewProductsByNameUseCase
    {
        private readonly IProductRepository productRepository;

        public ViewProductsByNameUseCase(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public async Task<IEnumerable<Product>> ExecuteAsync(string name = "")
        {
            return await productRepository.GetProductsByNameAsync(name);
        }
    }
}

