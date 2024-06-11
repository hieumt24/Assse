import { Button } from "@/components/ui/button";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import { RadioGroup, RadioGroupItem } from "@/components/ui/radio-group";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import { Separator } from "@/components/ui/separator";
import { GENDERS, LOCATIONS, ROLES } from "@/constants";
import { createUserSchema } from "@/validations";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { useNavigate } from "react-router-dom";
import { z } from "zod";

export const CreateUserForm = () => {
  // Define form
  const form = useForm<z.infer<typeof createUserSchema>>({
    mode: "all",
    resolver: zodResolver(createUserSchema),
    defaultValues: {
      firstName: "",
      lastName: "",
      dob: "",
      joinedDate: "",
      gender: "0",
      role: "1",
      location: "0",
    },
  });

  // Function handle onSubmit
  const onSubmit = (values: z.infer<typeof createUserSchema>) => {
    const gender = parseInt(values.gender);
    const role = parseInt(values.role);
    const location = parseInt(values.location);
    console.log({ ...values, gender, role, location });
  };

  const navigate = useNavigate();

  return (
    <Form {...form}>
      <form
        onSubmit={form.handleSubmit(onSubmit)}
        className="w-1/3 space-y-5 rounded-2xl bg-white p-6 shadow-lg"
      >
        <h1 className="text-center text-3xl font-bold uppercase">
          Create User
        </h1>
        <Separator />
        {/* First name */}
        <FormField
          control={form.control}
          name="firstName"
          render={({ field }) => (
            <FormItem>
              <FormLabel>First Name</FormLabel>
              <FormControl>
                <Input placeholder="Enter first name" {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        {/* Last name */}
        <FormField
          control={form.control}
          name="lastName"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Last Name</FormLabel>
              <FormControl>
                <Input placeholder="Enter last name" {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        {/* Date of birth */}
        <FormField
          control={form.control}
          name="dob"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Date of birth</FormLabel>
              <FormControl>
                <Input {...field} type="date" className="justify-center" />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        {/* Joined date */}
        <FormField
          control={form.control}
          name="joinedDate"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Joined Date</FormLabel>
              <FormControl>
                <Input {...field} type="date" className="justify-center" />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        {/* Gender */}
        <FormField
          control={form.control}
          name="gender"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Gender</FormLabel>
              <FormControl>
                <RadioGroup
                  onValueChange={field.onChange}
                  defaultValue={field.value}
                  className="flex"
                >
                  {GENDERS.map((gender) => {
                    return (
                      <FormItem
                        className="flex items-center space-x-3 space-y-0"
                        key={gender.value}
                      >
                        <FormControl>
                          <RadioGroupItem value={gender.value.toString()} />
                        </FormControl>
                        <FormLabel className="font-normal">
                          {gender.label}
                        </FormLabel>
                      </FormItem>
                    );
                  })}
                </RadioGroup>
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        {/* Role */}
        <FormField
          control={form.control}
          name="role"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Types</FormLabel>
              <FormControl>
                <Select value={field.value} onValueChange={field.onChange}>
                  <SelectTrigger>
                    <SelectValue placeholder="Role" />
                  </SelectTrigger>
                  <SelectContent>
                    {ROLES.map((role) => (
                      <SelectItem
                        key={role.value}
                        value={role.value.toString()}
                      >
                        {role.label}
                      </SelectItem>
                    ))}
                  </SelectContent>
                </Select>
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        {/* Location */}
        <FormField
          control={form.control}
          name="location"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Location</FormLabel>
              <FormControl>
                <RadioGroup
                  onValueChange={field.onChange}
                  defaultValue={field.value}
                  className="flex"
                >
                  {LOCATIONS.map((location) => {
                    return (
                      <FormItem
                        className="flex items-center space-x-3 space-y-0"
                        key={location.value}
                      >
                        <FormControl>
                          <RadioGroupItem value={location.value.toString()} />
                        </FormControl>
                        <FormLabel className="font-normal">
                          {location.label}
                        </FormLabel>
                      </FormItem>
                    );
                  })}
                </RadioGroup>
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <div className="flex justify-end gap-8">
          <Button
            type="submit"
            className="bg-red-500 hover:bg-white hover:text-red-500"
            disabled={!form.formState.isValid}
          >
            Save
          </Button>
          <Button
            type="button"
            onClick={() => {
              navigate("/admin/user");
            }}
          >
            Cancel
          </Button>
        </div>
      </form>
    </Form>
  );
};
