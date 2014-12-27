using AngularJSAuthentication.Data.Attributes;
using AngularJSAuthentication.Data.Infrastructure;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace AngularJSAuthentication.Data.Entities
{
    [CollectionName("GlassCredential")]
    public class GlassCredential : IEntity<string>
    {
        [Key]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public long ExpiresIn { get; set; }
        public string TokenType { get; set; }
        
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Issued { get; set; }
        public bool Active { get; set; }
    }
}
