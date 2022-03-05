﻿using CommonLayer.Models;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface ILabelRL
    {
        public bool CreateLabel(LabelModel labelModel);
        public IEnumerable<Label> GetAllLabels(long userId);
        public List<Label> GetByLabelID(long labelID);
    }
}