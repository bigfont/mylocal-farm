// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.ComponentModel.DataAnnotations;

namespace FarmApi.Models
{
    public class TodoItem : FarmEntity
    {
        [Required]
        public string Title { get; set; }
        public bool IsDone { get; set; }
    }
}
