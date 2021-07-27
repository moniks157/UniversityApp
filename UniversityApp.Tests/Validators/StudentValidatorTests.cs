using FluentValidation;
using FluentValidation.TestHelper;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityApp.DTOs;
using UniversityApp.Validators;

namespace UniversityApp.Tests.Validators
{
    [TestFixture]
    public class StudentValidatorTests
    {
        private IValidator<StudentDto> studentValidator;

        [SetUp]
        public void SetUp()
        {
            studentValidator = new StudentValidator();
        }

        //LastName

        [Test]
        public void Validate_Should_ReturnError_When_LastNameIsCamelCase()
        {
            //Arrange
            var model = new StudentDto { LastName = "kowalski" };

            //Act
            var result = studentValidator.TestValidate(model);

            //Assert
            result.ShouldHaveValidationErrorFor(student => student.LastName);
        }

        [Test]
        public void Validate_Should_ReturnError_When_LastNameHasNumbers()
        {
            //Arrange
            var model = new StudentDto { LastName = "Kowalski123" };

            //Act
            var result = studentValidator.TestValidate(model);

            //Assert
            result.ShouldHaveValidationErrorFor(student => student.LastName);
        }

        [Test]
        public void Validate_Should_ReturnError_When_LastNameHasSpecialSymbols()
        {
            //Arrange
            var model = new StudentDto { LastName = "Kow@lski" };

            //Act
            var result = studentValidator.TestValidate(model);

            //Assert
            result.ShouldHaveValidationErrorFor(student => student.LastName);
        }
        
        [Test]
        public void Validate_Should_ReturnError_When_LastNameIsTwoPartWithCamelCase()
        {
            //Arrange
            var model = new StudentDto { LastName = "Kowalski wrona" };

            //Act
            var result = studentValidator.TestValidate(model);

            //Assert
            result.ShouldHaveValidationErrorFor(student => student.LastName);
        }

        [Test]
        public void Validate_Should_ReturnError_When_LastNameIsTwoPartWithCamelCaseAndHyphen()
        {
            //Arrange
            var model = new StudentDto { LastName = "Kowalski-wrona" };

            //Act
            var result = studentValidator.TestValidate(model);

            //Assert
            result.ShouldHaveValidationErrorFor(student => student.LastName);
        }

        [Test]
        public void Validate_Should_ReturnNoError_When_LastNameIsPascalCase()
        {
            //Arrange
            var model = new StudentDto { LastName = "Kowalski" };

            //Act
            var result = studentValidator.TestValidate(model);

            //Assert
            result.ShouldNotHaveValidationErrorFor(student => student.LastName);
        }

        [Test]
        public void Validate_Should_ReturnNoError_When_LastNameIsTwoPartWithPascalCase()
        {
            //Arrange
            var model = new StudentDto { LastName = "Kowalski Wrona" };

            //Act
            var result = studentValidator.TestValidate(model);

            //Assert
            result.ShouldNotHaveValidationErrorFor(student => student.LastName);
        }

        [Test]
        public void Validate_Should_ReturnNoError_When_LastNameIsTwoPartWithPascalCaseAndHyphen()
        {
            //Arrange
            var model = new StudentDto { LastName = "Kowalski-Wrona" };

            //Act
            var result = studentValidator.TestValidate(model);

            //Assert
            result.ShouldNotHaveValidationErrorFor(student => student.LastName);
        }

        [Test]
        public void Validate_Should_ReturnNoError_When_LastNameIsThreePartWithPascalCaseAndHyphen()
        {
            //Arrange
            var model = new StudentDto { LastName = "Kowalski-Wrona-Ptak" };

            //Act
            var result = studentValidator.TestValidate(model);

            //Assert
            result.ShouldNotHaveValidationErrorFor(student => student.LastName);
        }

        //FirstName

        [Test]
        public void Validate_Should_ReturnError_When_NoFirstName()
        {
            //Arrange
            var model = new StudentDto { LastName = "Kowalski-Wrona-Ptak" };

            //Act
            var result = studentValidator.TestValidate(model);

            //Assert
            result.ShouldHaveValidationErrorFor(student => student.FirstName);
        }

        [Test]
        public void Validate_Should_ReturnError_When_FirstNameIsCamelCase()
        {
            //Arrange
            var model = new StudentDto { LastName = "Kowalski-Wrona-Ptak", FirstName = "agata" };

            //Act
            var result = studentValidator.TestValidate(model);

            //Assert
            result.ShouldHaveValidationErrorFor(student => student.FirstName);
        }

        [Test]
        public void Validate_Should_ReturnError_When_FirstNameHasNumbers()
        {
            //Arrange
            var model = new StudentDto { LastName = "Kowalski-Wrona-Ptak", FirstName = "Aga1ta3" };

            //Act
            var result = studentValidator.TestValidate(model);

            //Assert
            result.ShouldHaveValidationErrorFor(student => student.FirstName);
        }

        [Test]
        public void Validate_Should_ReturnError_When_FirstNameHasSpecialSymbols()
        {
            //Arrange
            var model = new StudentDto { LastName = "Kowalski-Wrona-Ptak", FirstName = "Aga*ta" };

            //Act
            var result = studentValidator.TestValidate(model);

            //Assert
            result.ShouldHaveValidationErrorFor(student => student.FirstName);
        }

        [Test]
        public void Validate_Should_ReturnError_When_FirstNameIsTwoPartWithCamelCase()
        {
            //Arrange
            var model = new StudentDto { LastName = "Kowalski-Wrona-Ptak", FirstName = "Aga ta" };

            //Act
            var result = studentValidator.TestValidate(model);

            //Assert
            result.ShouldHaveValidationErrorFor(student => student.FirstName);
        }

        [Test]
        public void Validate_Should_ReturnNoError_When_FirstNameIsPascalCase()
        {
            //Arrange
            var model = new StudentDto { LastName = "Kowalski-Wrona-Ptak", FirstName = "Aga" };

            //Act
            var result = studentValidator.TestValidate(model);

            //Assert
            result.ShouldNotHaveValidationErrorFor(student => student.FirstName);
        }

        [Test]
        public void Validate_Should_ReturnNoError_When_FirstNameHasTwoParts()
        {
            //Arrange
            var model = new StudentDto { LastName = "Kowalski-Wrona-Ptak", FirstName = "Agata Maria" };

            //Act
            var result = studentValidator.TestValidate(model);

            //Assert
            result.ShouldNotHaveValidationErrorFor(student => student.FirstName);
        }

        //Age

        [Test]
        public void Validate_Should_ReturnError_When_AgeIsBelowMin()
        {
            //Arrange
            var model = new StudentDto { Age = Constants.MIN_AGE - 1 };

            //Act
            var result = studentValidator.TestValidate(model);

            //Assert
            result.ShouldHaveValidationErrorFor(student => student.Age);
        }

        [Test]
        public void Validate_Should_ReturnError_When_AgeIsAboveMax()
        {
            //Arrange
            var model = new StudentDto { Age = Constants.MAX_AGE + 1 };

            //Act
            var result = studentValidator.TestValidate(model);

            //Assert
            result.ShouldHaveValidationErrorFor(student => student.Age);
        }

        [Test]
        public void Validate_Should_ReturnNoError_When_AgeIsInRange()
        {
            //Arrange
            var model = new StudentDto { Age = new Random().Next(Constants.MIN_AGE, Constants.MAX_AGE) };

            //Act
            var result = studentValidator.TestValidate(model);

            //Assert
            result.ShouldNotHaveValidationErrorFor(student => student.Age);
        }

        [Test]
        public void Validate_Should_ReturnNoError_When_AgeIsMin()
        {
            //Arrange
            var model = new StudentDto { Age = Constants.MIN_AGE};

            //Act
            var result = studentValidator.TestValidate(model);

            //Assert
            result.ShouldNotHaveValidationErrorFor(student => student.Age);
        }

        [Test]
        public void Validate_Should_ReturnNoError_When_AgeIsMax()
        {
            //Arrange
            var model = new StudentDto { Age = Constants.MAX_AGE };

            //Act
            var result = studentValidator.TestValidate(model);

            //Assert
            result.ShouldNotHaveValidationErrorFor(student => student.Age);
        }

        //IsAdult

        [Test]
        public void Validate_Should_ReturnError_When_IsAdultIsFalseForAgeOfAdulthood()
        {
            //Arrange
            var model = new StudentDto { Age = Constants.AGE_OF_ADULTHOOD, IsAdult = false };

            //Act
            var result = studentValidator.TestValidate(model);

            //Assert
            result.ShouldHaveValidationErrorFor(student => student.IsAdult);
        }

        [Test]
        public void Validate_Should_ReturnError_When_IsAdultIsTrueForAgeBelowAdulthood()
        {
            //Arrange
            var model = new StudentDto { Age = Constants.AGE_OF_ADULTHOOD - 1, IsAdult = true };

            //Act
            var result = studentValidator.TestValidate(model);

            //Assert
            result.ShouldHaveValidationErrorFor(student => student.IsAdult);
        }

        [Test]
        public void Validate_Should_ReturnError_When_IsAdultIsFalseForAgeAboveAdulthood()
        {
            //Arrange
            var model = new StudentDto { Age = Constants.AGE_OF_ADULTHOOD + 1, IsAdult = false };

            //Act
            var result = studentValidator.TestValidate(model);

            //Assert
            result.ShouldHaveValidationErrorFor(student => student.IsAdult);
        }
    }
}
