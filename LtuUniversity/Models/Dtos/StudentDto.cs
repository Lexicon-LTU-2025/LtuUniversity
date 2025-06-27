using LtuUniversity.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace LtuUniversity.Models.Dtos;

public record StudentDto(int Id, string FullName, string Avatar, string City);
