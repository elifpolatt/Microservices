using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FreeCourse.Shared.Dtos
{
    //API'lardan bir istek dönerken ya basarılı ya da basarısız dto nesnesi dönmektedir.
    //basarili ya da basarisiz oldugunda da ortak bir DTO nesnesi dönüldüğünde bu DTO nesnesi içerisinde basarılı ve hata kısmı doldurulur.
    //Tek DTO nesnesi dönecek.
    public class ResponseDto<T>
    {
        public T Data { get; set; } //API başarılı olduğunda dönecek olan veri

        [JsonIgnore] //sadece kendi icinde kullanmak icin 
        //API'e istek yapıldıgında responseında donus tipini verebiliyoruz.
        public int StatusCode { get; set; } //responseun kodunu belirlemek için

        [JsonIgnore]
        public bool IsSuccessfull { get; set; } //işlemin başarılı olup olmadığını belirlemek için kullanılır

        //bu iki prop ile client karsılasmayacak


        //basarısız olma durumunda client hataları alacak. birden fazla hata alabiliriz.
        public List<string> Errors { get; set; } //ErrorDto yerıne daha basit yapı


        //Response dto nesnesini üretmek için static metotlar tanımladım
        //Static metotlarla beraber yeni bir nesne dönülüyorsa bu Static Factory Method'dur.

        //başarılı olması ve kodu
        public static ResponseDto<T> Success(T data, int statusCode)
        {
            return new ResponseDto<T>()
            {
                Data = data,
                StatusCode = statusCode,
                IsSuccessfull = true
            };
        }
         
        //başarıı olabilir ama data almayabilir (update)
        public static ResponseDto<T> Success(int statusCode)
        {
            return new ResponseDto<T>
            {
                Data = default(T),
                StatusCode = statusCode,
                IsSuccessfull = true
            };
        }

        public static ResponseDto<T> Fail(List<string> errors, int statusCode)
        {
            return new ResponseDto<T>
            {
                Errors = errors,
                StatusCode = statusCode,
                IsSuccessfull = false
            };
        }
        //tek bir hata dönecek
        public static ResponseDto<T> Fail(string error, int statusCode)
        {
            return new ResponseDto<T>
            {
                Errors = new List<string>() { error },
                StatusCode = statusCode,
                IsSuccessfull = false
            };
        }
    }
}
