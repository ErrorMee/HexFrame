using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Google.Protobuf;
using Google.Protobuf.Examples.AddressBook;

public class ProtobufManager : SingletonBehaviour<ProtobufManager>
{

    public void Test()
    {

        byte[] bytes;
        // Create obj
        Person person = new Person
        {
            Id = 1,
            Name = "protobuf 测试通过",
            Email = "foo@bar",
            Phones = { new Person.Types.PhoneNumber { Number = "555-1212" } }
        };
        using (MemoryStream stream = new MemoryStream())
        {
            // Save to bytes
            person.WriteTo(stream);
            bytes = stream.ToArray();
        }
        //Read obj from bytes
        Person copy = Person.Parser.ParseFrom(bytes);

        AddressBook book = new AddressBook
        {
            People = { copy }
        };
        // Save to bytes
        bytes = book.ToByteArray();
        //Read obj from bytes
        AddressBook restored = AddressBook.Parser.ParseFrom(bytes);

        PrintMessage(restored);

        // The message performs a deep-comparison on equality:
        if (restored.People.Count != 1 || !person.Equals(restored.People[0]))
        {
            throw new Exception("There is a bad person in here!");
        }

    }

    public void PrintMessage(IMessage message)
    {
        if (GLog.isOpen)
        {
            var descriptor = message.Descriptor;
            foreach (var field in descriptor.Fields.InDeclarationOrder())
            {
                GLog.Log(string.Format("Field {0} ({1}): {2}", field.FieldNumber,
                    field.Name,
                    field.Accessor.GetValue(message)));
            }
        }
    }

}
