#!/bin/bash
dotnet ef database update
dotnet watch run --urls=http://+:5000
