using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.DataObject
{
    public class CustomerDO
    {
        #region Fields

        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier card.
        /// </summary>
        /// <value>
        /// The identifier card.
        /// </value>
        public String IdCard { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public String Name { get; set; }

        /// <summary>
        /// Gets or sets the surname.
        /// </summary>
        /// <value>
        /// The surname.
        /// </value>
        public String Surname { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Int32 ExternalId { get; set; }

        /// <summary>
        /// Account Balance
        /// </summary>
        public decimal Balance { get; set; }

        #endregion

    }
}