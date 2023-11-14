﻿using Loginoperations.Context;
using Loginoperations.Dto;
using Loginoperations.Dto.Order;
using Loginoperations.Dto.Product;
using Loginoperations.Enum;
using Loginoperations.Model;
using Loginoperations.Model.Order;
using Microsoft.AspNetCore.Mvc;

namespace Loginoperations.Controllers;
[ApiController]
[Route("api/v1/[controller]")]
public class OrderController : ControllerBase
{
    private readonly DContext _context;
    private readonly DtoConverter _converter;

    public OrderController(DContext context,DtoConverter converter)
    {
        _context = context;
        _converter = converter;
    }

    // sipariş al
    [HttpPost("create/{userId}/{productId}")]
    public async Task<IActionResult> CreateOrder(int userId,int productId)
    {
        var dbUser=_context.Users.FirstOrDefault(x=>x.Id==userId);
        var dbProduct=_context.Products.FirstOrDefault(x=>x.Id==productId);
        if (dbUser == null || dbProduct == null)
        {
            return BadRequest("Ürün sepete eklenemiyor");
        }

        Order order = new Order();
        order.ProductId = productId;
        order.UserId = userId;
        order.User = dbUser;
        order.Product = dbProduct;
        order.OrderStatus = OrderStatus.SİPARİŞALINDI.ToString();
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return Ok("Sipariş alındı");
    }
    
    // sipariş iptal et
    [HttpDelete("cancel/{orderId}")]
    public async Task<IActionResult> CancelOrder(int orderId)
    {
        var db = _context.Orders.FirstOrDefault(x => x.Id == orderId);
        if (db == null)
        {
            return BadRequest("Sipariş Bulunamadi");
        }

        _context.Orders.Remove(db);
        await _context.SaveChangesAsync();
        return Ok("Sipariş iptal edildi");
    }
   
    
    // sipariş durumu güncelle
    [HttpPatch("updateOrderStatus")]
    public async Task<IActionResult> updateOrderStatus(int orderId,[FromBody] UpdateStatusRequest update)
    {
        var db = _context.Orders.FirstOrDefault(x => x.Id == orderId);
        if (db == null)
        {
            return BadRequest("Sipariş Bulunamadi");
        }

        db.OrderStatus = update.Status.ToString();
        _context.Orders.Update(db);
        await _context.SaveChangesAsync();
        return Ok("Durumu güncellendi");
    }
    
    // kullanıcıya ait siparişleri listele
    [HttpGet("{userId}")]
    public async Task<List<ProductsResponse>> getMyOrders(int userId)
    {
        List<Order> orders = _context.Orders.ToList();
        List<ProductsResponse> products = new List<ProductsResponse>();
        foreach (var order in orders)
        {
            if (order != null)
            {
                if (order.UserId == userId)
                {
                    var product = _context.Products.FirstOrDefault(x => x.Id == order.ProductId);
                    products.Add(_converter.convertProduct(product));
                }
            }
        }

        return products;
    }
    // kullaniciya gelen siparişleri görüntüle
    [HttpGet("getOrders/{userId}")]
    public async Task<List<ProductsResponse>> getOrders(int userId)
    {
        List<Order> orders = _context.Orders.ToList();
        List<ProductsResponse> products = new List<ProductsResponse>();
        foreach (var order in orders)
        {
            if (order != null)
            {
                if (order.Product.UserId == userId)
                {
                    var product = _context.Products.FirstOrDefault(x => x.Id == order.ProductId);
                    products.Add(_converter.convertProduct(product));
                }
            }
        }

        return products;
    }
    
}