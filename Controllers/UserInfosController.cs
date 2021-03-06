﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Webbanhang.Models;

namespace Webbanhang.Controllers
{
    public class UserInfosController : ApiController
    {
        [HttpGet]
        //[Authorize]
        public HttpResponseMessage LoadAllUserInfo(string take = null, string userid = null, string ten = null, string email =null)
        {
            try
            {
                using (WebbanhangDBEntities entities = new WebbanhangDBEntities())
                {
                    entities.Configuration.ProxyCreationEnabled = false;
                    var result = entities.UserInfos.Select(x=>new {x.UserInfoID, x.UserID, x.Name, x.PhoneNumber, x.HomeAddress, x.CMND, LoginEmail = entities.AspNetUsers.FirstOrDefault(y=>y.Id == x.UserID).UserName}).ToList();
                    result = result.OrderByDescending(x => x.UserInfoID).ToList();
                    if(userid !=null)
                    {
                        result = result.Where(x => x.UserID == userid).ToList();
                    }
                    if(ten != null)
                    {
                        result = result.Where(x => x.Name == ten).ToList();
                    }
                    if(email !=null)
                    {
                        result = result.Where(x => x.LoginEmail == email).ToList();
                    }
                    if (take != null)
                    {
                        int tempTake = Convert.ToInt32(take);
                        result = result.Take(tempTake).ToList();
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [HttpGet]
        [Authorize]
        [Route("api/UserInfos/Cart")]
        public HttpResponseMessage GetCart()
        {
            try
            {
                using (WebbanhangDBEntities entities = new WebbanhangDBEntities())
                {
                    entities.Configuration.ProxyCreationEnabled = false;
                    string userid = HttpContext.Current.User.Identity.GetUserId();
                    var entity = entities.UserInfos.FirstOrDefault(e => e.UserID == userid).Cart;
                    if (entity != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, userid + "not found");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [HttpGet]
        [Authorize]
        public HttpResponseMessage LoadUserInfoByID(int id)
        {
            try
            {
                using (WebbanhangDBEntities entities = new WebbanhangDBEntities())
                {
                    entities.Configuration.ProxyCreationEnabled = false;
                    var entity = entities.UserInfos.FirstOrDefault(e => e.UserInfoID == id);
                    if (entity != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, id.ToString() + "not found");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpGet]
        [Authorize]
        [Route("api/UserInfos/LoadSoLuongTinVipConLai")]
        public HttpResponseMessage LoadSoLuongTinVipConLai()
        {
            try
            {
                using (WebbanhangDBEntities entities = new WebbanhangDBEntities())
                {
                    entities.Configuration.ProxyCreationEnabled = false;
                    string currentuid = User.Identity.GetUserId();
                    return Request.CreateResponse(HttpStatusCode.OK, entities.UserInfos.FirstOrDefault(x => x.UserID == currentuid).VipNewsCount);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpGet]
        [Authorize]
        [Route("api/UserInfos/LoadUserInfoByUserID")]
        public HttpResponseMessage LoadUserInfoByUserID(string uid)
        {
            try
            {
                using (WebbanhangDBEntities entities = new WebbanhangDBEntities())
                {
                    entities.Configuration.ProxyCreationEnabled = false;
                    var entity = entities.UserInfos.FirstOrDefault(e => e.UserID == uid);
                    if (entity != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, uid.ToString() + "not found");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpGet]
        [Route("api/UserInfos/CurrentUserInfo")]
        [Authorize]
        public HttpResponseMessage GetCurrentUserInfo()
        {
            try
            {
                using (WebbanhangDBEntities entities = new WebbanhangDBEntities())
                {
                    entities.Configuration.ProxyCreationEnabled = false;
                    string uid = User.Identity.GetUserId();
                    var entity = entities.UserInfos.FirstOrDefault(e => e.UserID == uid);
                    if (entity != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, User.Identity.GetUserId().ToString() + "not found");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPut]
        [Route("api/UserInfos/CurrentUserInfo")]
        [Authorize]
        public HttpResponseMessage EditCurrentUserInfo([FromBody]UserinfoModel userinfo)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                using (WebbanhangDBEntities entities = new WebbanhangDBEntities())
                {
                    entities.Configuration.ProxyCreationEnabled = false;
                    string uid = User.Identity.GetUserId();
                    var entity = entities.UserInfos.FirstOrDefault(e => e.UserID == uid);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest,"Có lỗi xảy ra");
                    }
                    else
                    {
                        entity.Name = userinfo.Name;
                        entity.HomeAddress = userinfo.HomeAddress;
                        entity.Email = userinfo.Email;
                        entity.PhoneNumber = userinfo.PhoneNumber;
                        entity.CMND = userinfo.CMND;
                        entities.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK, "Đã sửa");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // api/product
        [HttpPost]
        [Authorize]
        public HttpResponseMessage Post([FromBody] UserInfo userinfo)
        {
            try
            {
                using (WebbanhangDBEntities entities = new WebbanhangDBEntities())
                {
                    entities.Configuration.ProxyCreationEnabled = false;
                    userinfo.UserID = User.Identity.GetUserId();
                    entities.UserInfos.Add(userinfo);
                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, "POST OK");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // api/product/1
        [HttpPut]
        [Authorize]
        public HttpResponseMessage Put(int id, [FromBody]UserinfoModel userinfo)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

                using (WebbanhangDBEntities entities = new WebbanhangDBEntities())
                {
                    entities.Configuration.ProxyCreationEnabled = false;
                    var entity = entities.UserInfos.FirstOrDefault(e => e.UserInfoID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "User Info with Id " + id.ToString() + " not found to update");
                    }
                    else
                    {
                        entity.Name = userinfo.Name;
                        entity.HomeAddress = userinfo.HomeAddress;
                        entity.Email = userinfo.Email;
                        entity.PhoneNumber = userinfo.PhoneNumber;
                        entity.CMND = userinfo.CMND;
                        entities.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK, "Edited");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // api/product/1
        [HttpDelete]
        [Authorize]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (WebbanhangDBEntities entities = new WebbanhangDBEntities())
                {
                    entities.Configuration.ProxyCreationEnabled = false;
                    var entity = entities.UserInfos.FirstOrDefault(e => e.UserInfoID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Userinfo with Id = " + id.ToString() + " not found to delete");
                    }
                    else
                    {
                        entities.UserInfos.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, "Delete OK");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpGet]
        [Route("api/Userinfos/GetUserNameByProductID")]
        public HttpResponseMessage GetUserNameByProductID([FromUri] int pid)
        {
            try
            {
                using (WebbanhangDBEntities entities = new WebbanhangDBEntities())
                {
                    entities.Configuration.ProxyCreationEnabled = false;
                    var producttoFind = entities.Products.FirstOrDefault(x => x.ProductID == pid);
                    string sellerID = producttoFind.UserID;
                    string result = entities.UserInfos.FirstOrDefault(x => x.UserID == sellerID).Name;
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpGet]
        [Route("api/Userinfos/GetUserMailByUserID")]
        public HttpResponseMessage GetUserMailByUserID([FromUri] string uid)
        {
            try
            {
                using (WebbanhangDBEntities entities = new WebbanhangDBEntities())
                {
                    entities.Configuration.ProxyCreationEnabled = false;
                    return Request.CreateResponse(HttpStatusCode.OK, entities.AspNetUsers.FirstOrDefault(x => x.Id == uid).UserName);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
