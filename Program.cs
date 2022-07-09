using Microsoft.Data.Sqlite;
using Avaliacao3BimLp3.Database;
using Avaliacao3BimLp3.Models;
using Avaliacao3BimLp3.Repositories;

var databaseConfig = new DatabaseConfig();
var databaseSetup = new DatabaseSetup(databaseConfig);

var studentRepository = new StudentRepository(databaseConfig);

var modelName = args[0];
var modelAction = args[1];

if(modelName == "Student")
{
    if(modelAction == "New")
    {
        var registration = args[2];
        var name = args[3];
        var city  = args[4];
        var former = false;

        var student = new Student(registration, name, city , former);
        if(studentRepository.ExistsById(registration))
        {
            Console.WriteLine($"Estudante com Registration {registration} já existe");
        }else
        {
            studentRepository.Save(student);
            Console.WriteLine($"Estudante {name} cadastrado com sucesso");
        }
    }

    if(modelAction == "Delete")
    {
        string registration = args[2];

        if(studentRepository.ExistsById(registration))
        {
            studentRepository.Delete(registration);
            Console.WriteLine($"Estudante {registration} removido com sucesso");
        }else
        {
            Console.WriteLine($"Estudante {registration} não encontrado!");
        }
    }

    if(modelAction == "MarkAsFormed")
    {
        string registration = args[2];

        if(studentRepository.ExistsById(registration))
        {
            studentRepository.MarkAsFormed(registration);
            Console.WriteLine($"Estudante {registration} definido com sucesso");
        }else
        {
            Console.WriteLine($"Estudante {registration} não encontrado!");
        }
    }

    if(modelAction == "List")
    {
        if(studentRepository.GetAll().Any())
        {
            Console.WriteLine("Student List");
            foreach (var student in studentRepository.GetAll())
            {
                Console.WriteLine($"{student.Registration}, {student.Name}, {student.City}, {student.Former}");
            } 
        }else
        {
            Console.WriteLine("Não encontrado");
        } 
    }

    if(modelAction == "ListFormed")
    {
        if(studentRepository.GetAllFormed().Any())
        {
            Console.WriteLine("Student List");
            foreach (var student in studentRepository.GetAllFormed())
            {
                Console.WriteLine($"{student.Registration}, {student.Name}, {student.City}, {student.Former}");
            } 
        }else
        {
            Console.WriteLine("Não encontrado");
        } 
    }

    if(modelAction == "ListByCity")
    {
        string city = args[4];

        if(studentRepository.GetAllStudentByCity(city).Any())
        {
            Console.WriteLine("Student List");
            foreach (var student in studentRepository.GetAllStudentByCity(city))
            {
                Console.WriteLine($"{student.Registration}, {student.Name}, {student.City}, {student.Former}");
            } 
        }else
        {
            Console.WriteLine("Não encontrado");
        } 
    }

    if(modelAction == "ListByCities")
    {
	    String[] cities = new String[args.GetLength(1)-3];
        
        for(int i = 2; i < args.GetLength(1); i++)
        {
            cities[i] = args[i];
        }

        if(studentRepository.GetAllByCities(cities).Any())
        {
            Console.WriteLine("Student List");
            foreach (var student in studentRepository.GetAllByCities(cities))
            {
                Console.WriteLine($"{student.Registration}, {student.Name}, {student.City}, {student.Former}");
            } 
        }else
        {
            Console.WriteLine("Não encontrado");
        } 
    }

}
