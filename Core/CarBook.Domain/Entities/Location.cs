﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Domain.Entities
{
    public class Location
    {
        [Key]
        public int LocationID { get; set; }
        public string LocationName { get; set; }
        public List<RentACar> RentACar { get; set; }
        public List<Reservation> PickUpReservations { get; set; }
        public List<Reservation> DropOffReservations { get; set; }
    }
}
